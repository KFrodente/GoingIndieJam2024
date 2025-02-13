using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : Room
{
    public static int basicRoomProcesses = 5;

    [SerializeField] private Tile dirt;
    [SerializeField] private RuleTile ground;
    [SerializeField] private Tile portalGround;
    [SerializeField] private RuleTile wall;

    [SerializeField, Range(0, 1)] private float dirtPatchChance;

    [SerializeField] private Grid[,] gridHandler;
    private List<Vector2Int> grounds = new();
    private List<Vector2Int> goodEnemyPositions = new();
    [SerializeField] private List<WalkerObject> walkers;

    [SerializeField] private int maxWalkers;
    private int tileCount;

    public Vector2 roomOffset;

    private FloorStatsSO floorStats;

    private bool previouslyActivated = false;

    private Vector2Int highestPos;
    private Vector2Int rightmostPos;
    private Vector2Int lowestPos;
    private Vector2Int leftmostPos;

    private void Start()
    {
        tilemap = FloorGenerator.instance.globalTilemap;
        floorStats = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum];
        InitializeGrid();
    }

    public void SetTilesActive()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            if (gridHandler[grounds[i].x, grounds[i].y] == Grid.GRASS)
                tilemap.SetTile(new Vector3Int(grounds[i].x + (int)roomOffset.x, grounds[i].y + (int)roomOffset.y), ground);
            else if (gridHandler[grounds[i].x, grounds[i].y] == Grid.DIRT)
                tilemap.SetTile(new Vector3Int(grounds[i].x + (int)roomOffset.x, grounds[i].y + (int)roomOffset.y), dirt);

            if (gridHandler[grounds[i].x, grounds[i].y] == Grid.PFLOOR)
                tilemap.SetTile(new Vector3Int(grounds[i].x + (int)roomOffset.x, grounds[i].y + (int)roomOffset.y), dirt);
                //tilemap.SetTile(new Vector3Int(grounds[i].x + (int)roomOffset.x, grounds[i].y + (int)roomOffset.y), portalGround);
        }
    }

    public IEnumerator SetRoomActive(BaseCharacter character, Portal connectedPortal, Room currentRoom)
    {
        int x = 0;
        int y = 0;

        StartCoroutine(TransitionManager.instance.FadeToBlack());

        yield return new WaitUntil(() => TransitionManager.instance.blackScreen.color.a >= 1);

        character.transform.position = connectedPortal.transform.position;

        while (x < gridHandler.GetLength(0))
        {
            while (y < gridHandler.GetLength(1))
            {
                if (gridHandler[x, y] == Grid.WALL)
                    tilemap.SetTile(new Vector3Int(x + (int)roomOffset.x, y + (int)roomOffset.y), wall);
                y++;
            }
            y = 0;
            x++;
        }

        yield return new WaitForSecondsRealtime(.2f);

        if (!previouslyActivated)
        {
            StartFight();

        }
        
        

        StartCoroutine(TransitionManager.instance.FadeOutOfBlack());

        yield return null;
    }

    private void StartFight()
    {
            Debug.Log("pls pls");
            GetComponent<EnemySpawner>().SpawnEnemies(goodEnemyPositions, roomOffset);
            SetPortalActivity(false);


        previouslyActivated = true;
    }

    public void SetPortalActivity(bool active)
    {
        if (topPortal != null) topPortal.gameObject.SetActive(active);
        if (rightPortal != null) rightPortal.gameObject.SetActive(active);
        if (bottomPortal != null) bottomPortal.gameObject.SetActive(active);
        if (leftPortal != null) leftPortal.gameObject.SetActive(active);
    }

    private void InitializeGrid()
    {
        gridHandler = new Grid[floorStats.roomWidth, floorStats.roomHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x, y] = Grid.WALL;
                //tilemap.SetTile(new Vector3Int(x + (int)roomOffset.x, y + (int)roomOffset.y), wall);
            }
        }

        walkers = new List<WalkerObject>();

        Vector2Int tileCenter = new Vector2Int(gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2);

        Vector2 pickedDir = GetDirection();
        WalkerObject currentWalker = new WalkerObject(new Vector2(tileCenter.x, tileCenter.y), pickedDir);

        gridHandler[tileCenter.x, tileCenter.y] = Grid.GRASS;
        //tilemap.SetTile(new Vector3Int(tileCenter.x + (int)roomOffset.x, tileCenter.y + (int)roomOffset.y), ground);

        grounds.Add(tileCenter);
        walkers.Add(currentWalker);

        tileCount++;

        rightmostPos = tileCenter;
        highestPos = tileCenter;
        leftmostPos = tileCenter;
        lowestPos = tileCenter;


        FloorGenerator.instance.roomProcessesFinished++;

        StartCoroutine(CreateFloors());
    }

    private Vector2 GetDirection()
    {
        int pickedDir = Random.Range(0, 4);

        switch (pickedDir)
        {
            case 0: return Vector2.down;
            case 1: return Vector2.up;
            case 2: return Vector2.left;
            case 3: return Vector2.right;
            default: return Vector2.zero;
        }
    }

    private IEnumerator CreateFloors()
    {
        while (tileCount / (float)gridHandler.Length < floorStats.fillPercentage)
        {

            foreach (WalkerObject walker in walkers)
            {
                Vector2Int pos = new Vector2Int((int)walker.position.x, (int)walker.position.y);

                if (gridHandler[pos.x, pos.y] != Grid.GRASS || gridHandler[pos.x, pos.y] != Grid.DIRT)
                {
                    //tilemap.SetTile(new Vector3Int(pos.x + (int)roomOffset.x, pos.y + (int)roomOffset.y), ground);
                    tileCount++;
                    if (pos.x > rightmostPos.x) rightmostPos = pos;
                    if (pos.x < leftmostPos.x) leftmostPos = pos;
                    if (pos.y > highestPos.y) highestPos = pos;
                    if (pos.y < lowestPos.y) lowestPos = pos;

                    if (Random.Range(0.0f, 1.0f) <= dirtPatchChance)
                    {
                        MakeDirtPatch(pos);
                    }
                    else
                        gridHandler[pos.x, pos.y] = Grid.GRASS;
                    grounds.Add(pos);
                }
            }

            CheckRemove();
            CheckRedirect();
            CheckCreate();
            UpdatePosition();
        }
        FloorGenerator.instance.roomProcessesFinished++;

        //CreateWalls();
        for (int i = 0; i < floorStats.timesToThicken; i++)
        {
            StartCoroutine(Thicken());
        }
        FloorGenerator.instance.roomProcessesFinished++;


        StartCoroutine(SimpleBulk());
        StartCoroutine(SimpleBulk());

        SetPortalPos();


        GeneratePortals();

        GetEnemyTiles();

        SetTilesActive();

        //StartCoroutine(FillWalls());

        yield return null;
    }

    private void MakeDirtPatch(Vector2Int pos)
    {
        int pattern = Random.Range(0, 3);
        int x = pos.x;
        int y = pos.y;
        switch(pattern)
        {
            case 0:
                MakeDirtTile(x, y);
                MakeDirtTile(x + 1, y);
                MakeDirtTile(x + 1, y + 1);
                MakeDirtTile(x, y + 1);
                MakeDirtTile(x, y - 1);
                MakeDirtTile(x + 1, y - 1);
                MakeDirtTile(x + 1, y - 2);
                MakeDirtTile(x - 1, y + 1);
                MakeDirtTile(x, y - 2);
                
                break;
            case 1:
                MakeDirtTile(x, y);
                MakeDirtTile(x - 1, y);
                MakeDirtTile(x - 1, y - 1);
                MakeDirtTile(x - 2, y);
                MakeDirtTile(x + 1, y);
                MakeDirtTile(x, y - 1);
                MakeDirtTile(x, y - 2);
                MakeDirtTile(x - 1, y - 2);

                break;
            case 2:
                MakeDirtTile(x, y);
                MakeDirtTile(x, y + 1);
                MakeDirtTile(x + 1, y + 1);
                MakeDirtTile(x + 1, y + 2);
                MakeDirtTile(x, y + 2);
                MakeDirtTile(x - 1, y + 2);
                MakeDirtTile(x + 2, y + 1);
                MakeDirtTile(x - 1, y);

                break;

        }
    }

    private void GetEnemyTiles()
    {
        for (int x = 1; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (GetIsFloor(x - 1, y + 1) && GetIsFloor(x, y + 1) && GetIsFloor(x + 1, y + 1) && GetIsFloor(x - 1, y) && GetIsFloor(x + 1, y) && GetIsFloor(x - 1, y - 1) && GetIsFloor(x, y - 1) && GetIsFloor(x + 1, y + 1) && GetIsFloor(x, y))
                {
                    goodEnemyPositions.Add(new Vector2Int(x, y));
                }
            }
        }
    }

    private IEnumerator Thicken()
    {
        int originalCount = grounds.Count;
        for (int i = 0; i < originalCount; i++)
        {
            int x = grounds[i].x;
            int y = grounds[i].y;
            if (gridHandler[x + 1, y] == Grid.WALL && gridHandler[x, y + 1] == Grid.WALL && Random.Range(0.0f, 1.0f) <= floorStats.chanceToThicken)
            {
                MakeRockTile(x + 1, y + 1);
            }
            if (gridHandler[x + 1, y] == Grid.WALL && gridHandler[x, y - 1] == Grid.WALL && Random.Range(0.0f, 1.0f) <= floorStats.chanceToThicken)
            {
                MakeRockTile(x + 1, y - 1);
            }
            if (gridHandler[x - 1, y] == Grid.WALL && gridHandler[x, y + 1] == Grid.WALL && Random.Range(0.0f, 1.0f) <= floorStats.chanceToThicken)
            {
                MakeRockTile(x - 1, y + 1);
            }
        }
        yield return null;
    }

    private IEnumerator SimpleBulk()
    {
        int originalCount = grounds.Count;
        for (int i = 0; i < originalCount; i++)
        {
            int x = grounds[i].x;
            int y = grounds[i].y;

            if (!GetIsFloor(x - 1, y))
            {
                MakeRockTile(x - 1, y);
            }
            if (!GetIsFloor(x, y - 1))
            {
                MakeRockTile(x, y - 1);
            }
        }
        yield return null;
    }

    private bool GetIsFloor(int x, int y)
    {
        return (gridHandler[x, y] == Grid.DIRT || gridHandler[x, y] == Grid.GRASS) && gridHandler[x, y] != Grid.PFLOOR;
    }

    private void CheckRemove()
    {
        int updatedCount = walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (Random.Range(0f, 1f) < floorStats.removeChance && walkers.Count > 1)
            {
                walkers.RemoveAt(i);
                break;
            }
        }
    }

    private void CheckRedirect()
    {
        foreach (WalkerObject walker in walkers)
        {
            if (Random.Range(0f, 1f) < floorStats.redirectChance)
            {
                Vector2 pickedDir = GetDirection();
                walker.direction = pickedDir;
            }
        }
    }

    private void CheckCreate()
    {
        int originalCount = walkers.Count;
        for (int i = 0; i < originalCount; i++)
        {
            if (Random.Range(0f, 1f) < floorStats.createChance && walkers.Count < maxWalkers)
            {
                Vector2 newDir = GetDirection();
                Vector2 newPos = walkers[i].position;

                WalkerObject newWalker = new WalkerObject(newPos, newDir);
                walkers.Add(newWalker);
            }
        }
    }

    private void UpdatePosition()
    {
        foreach (WalkerObject walker in walkers)
        {
            walker.position += walker.direction;
            walker.position.x = Mathf.Clamp(walker.position.x, 1, gridHandler.GetLength(0) - 2);
            walker.position.y = Mathf.Clamp(walker.position.y, 1, gridHandler.GetLength(1) - 2);
        }
    }

    private void SetPortalPos()
    {
        if (Vector2.Distance(highestPos, rightmostPos) <= 2)
        {
            highestPos += Vector2Int.up;
            rightmostPos += Vector2Int.right;
        }
        if (Vector2.Distance(rightmostPos, lowestPos) <= 2)
        {
            rightmostPos += Vector2Int.right;
            lowestPos += Vector2Int.down;
        }
        if (Vector2.Distance(lowestPos, leftmostPos) <= 2)
        {
            leftmostPos += Vector2Int.left;
            lowestPos += Vector2Int.down;
        }
        if (Vector2.Distance(leftmostPos, highestPos) <= 2)
        {
            leftmostPos += Vector2Int.left;
            highestPos += Vector2Int.up;
        }

        OpenAreas();
    }

    private void OpenAreas()
    {
        if (connectsUp) Clear3By3(highestPos);
        if (connectsRight) Clear3By3(rightmostPos);
        if (connectsDown) Clear3By3(lowestPos);
        if (connectsLeft) Clear3By3(leftmostPos);
    }

    private void Clear3By3(Vector2Int center)
    {
        int x = center.x;
        int y = center.y;
        //main 3x3
        MakePortalTile(x, y);
        MakePortalTile(x - 1, y + 1);
        MakePortalTile(x, y + 1);
        MakePortalTile(x + 1, y + 1);
        MakePortalTile(x - 1, y);
        MakePortalTile(x + 1, y);
        MakePortalTile(x - 1, y - 1);
        MakePortalTile(x, y - 1);
        MakePortalTile(x + 1, y - 1);


        //outside bits
        //top 3
        MakePortalTile(x - 1, y + 2);
        MakePortalTile(x, y + 2);
        MakePortalTile(x + 1, y + 2);
        //right 3
        MakePortalTile(x + 2, y + 1);
        MakePortalTile(x + 2, y);
        MakePortalTile(x + 2, y - 1);
        //left 3
        MakePortalTile(x - 2, y - 1);
        MakePortalTile(x - 2, y);
        MakePortalTile(x - 2, y + 1);
        //bottom 3
        MakePortalTile(x - 1, y - 2);
        MakePortalTile(x, y - 2);
        MakePortalTile(x + 1, y - 2);
    }

    private void MakeRockTile(int x, int y)
    {
        gridHandler[x, y] = Grid.GRASS;
        grounds.Add(new Vector2Int(x, y));
    }

    private void MakeDirtTile(int x, int y)
    {
        gridHandler[x, y] = Grid.DIRT;
        grounds.Add(new Vector2Int(x, y));
    }

    private void MakePortalTile(int x, int y)
    {
        gridHandler[x, y] = Grid.PFLOOR;
        grounds.Add(new Vector2Int(x, y));
    }

    public override void GeneratePortals()
    {
        if (connectsUp) topPortal = GenerateRespectivePortal(roomConnectedUp, new Vector2(highestPos.x + roomOffset.x + .5f, highestPos.y + roomOffset.y + .5f)).GetComponent<Portal>();
        if (connectsRight) rightPortal = GenerateRespectivePortal(roomConnectedRight, new Vector2(rightmostPos.x + roomOffset.x + .5f, rightmostPos.y + roomOffset.y + .5f)).GetComponent<Portal>();
        if (connectsDown) bottomPortal = GenerateRespectivePortal(roomConnectedDown, new Vector2(lowestPos.x + roomOffset.x + .5f, lowestPos.y + roomOffset.y + .5f)).GetComponent<Portal>();
        if (connectsLeft) leftPortal = GenerateRespectivePortal(roomConnectedLeft, new Vector2(leftmostPos.x + roomOffset.x + .5f, leftmostPos.y + roomOffset.y + .5f)).GetComponent<Portal>();
        
        FloorGenerator.instance.roomProcessesFinished++;
    }
}
