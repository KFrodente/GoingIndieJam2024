using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : Room
{
    

    public static int basicRoomProcesses = 5;

    [SerializeField] private Grid[,] gridHandler;
    private List<Vector2Int> grounds = new();
    [SerializeField] private List<WalkerObject> walkers;
    [SerializeField] private Tile ground;
    [SerializeField] private RuleTile wall;

    [SerializeField] private int maxWalkers;
    private int tileCount;

    public Vector2 roomOffset;

    private FloorStatsSO floorStats;
    private void Start()
    {
       
            tilemap = FloorGenerator.instance.globalTilemap;
        floorStats = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum];
        StartCoroutine(InitializeGrid());
    }

    public void SetTilesActive()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            tilemap.SetTile(new Vector3Int(grounds[i].x + (int)roomOffset.x, grounds[i].y + (int)roomOffset.y), ground);
        }
    }

    public void SetTilesInactive()
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            tilemap.SetTile(new Vector3Int(grounds[i].x + (int)roomOffset.x, grounds[i].y + (int)roomOffset.y), wall);
        }
    }

    IEnumerator InitializeGrid()
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

        gridHandler[tileCenter.x, tileCenter.y] = Grid.FLOOR;
        //tilemap.SetTile(new Vector3Int(tileCenter.x + (int)roomOffset.x, tileCenter.y + (int)roomOffset.y), ground);

        grounds.Add(tileCenter);
        walkers.Add(currentWalker);

        tileCount++;

        FloorGenerator.instance.roomProcessesFinished++;

        StartCoroutine(CreateFloors());
        yield return null;
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

                if (gridHandler[pos.x, pos.y] != Grid.FLOOR)
                {
                    //tilemap.SetTile(new Vector3Int(pos.x + (int)roomOffset.x, pos.y + (int)roomOffset.y), ground);
                    tileCount++;
                    gridHandler[pos.x, pos.y] = Grid.FLOOR;
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

        //if (Random.Range(0f, 1f) < .5f)
        //{
            SetTilesActive();
        //}
        //else
        //{
        //    SetTilesInactive();
        //}

        yield return null;
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
                //tilemap.SetTile(new Vector3Int(x + (int)roomOffset.x, y + (int)roomOffset.y), ground);
                gridHandler[x + 1, y + 1] = Grid.FLOOR;
                grounds.Add(new Vector2Int(x, y));
            }
            if (gridHandler[x + 1, y] == Grid.WALL && gridHandler[x, y - 1] == Grid.WALL && Random.Range(0.0f, 1.0f) <= floorStats.chanceToThicken)
            {
                //tilemap.SetTile(new Vector3Int(x + (int)roomOffset.x, y + (int)roomOffset.y), ground);
                gridHandler[x + 1, y - 1] = Grid.FLOOR;
                grounds.Add(new Vector2Int(x, y));
            }
            if (gridHandler[x - 1, y] == Grid.WALL && gridHandler[x, y + 1] == Grid.WALL && Random.Range(0.0f, 1.0f) <= floorStats.chanceToThicken)
            {
                //tilemap.SetTile(new Vector3Int(x + (int)roomOffset.x, y + (int)roomOffset.y), ground);
                gridHandler[x - 1, y + 1] = Grid.FLOOR;
                grounds.Add(new Vector2Int(x, y));
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
                gridHandler[x - 1, y] = Grid.FLOOR;
                grounds.Add(new Vector2Int(x - 1, y));
            }
            if (!GetIsFloor(x, y - 1))
            {
                gridHandler[x, y - 1] = Grid.FLOOR;
                grounds.Add(new Vector2Int(x, y - 1));
            }
        }
        yield return null;
    }


    private IEnumerator Bulk()
    {
        int originalCount = grounds.Count;
        for (int i = 0; i < originalCount; i++)
        {
            int x = grounds[i].x;
            int y = grounds[i].y;
            if (x > 1 && y > 1)
            {
                if (!GetIsFloor(x, y + 1) && !GetIsFloor(x, y - 1))
                {
                    //tilemap.SetTile(new Vector3Int(x + (int)roomOffset.x, y - 1 + (int)roomOffset.y), ground);
                    gridHandler[x, y - 1] = Grid.FLOOR;
                    grounds.Add(new Vector2Int(x, y - 1));
                }
                if (!GetIsFloor(x + 1, y) && !GetIsFloor(x - 1, y))
                {
                    //tilemap.SetTile(new Vector3Int(x + 1 + (int)roomOffset.x, y + (int)roomOffset.y), ground);
                    gridHandler[x + 1, y] = Grid.FLOOR;
                    grounds.Add(new Vector2Int(x + 1, y));
                }

                if (!GetIsFloor(x + 1, y + 1) && !GetIsFloor(x - 1, y - 1))
                {
                    //tilemap.SetTile(new Vector3Int(x - 1 + (int)roomOffset.x, y - 1 + (int)roomOffset.y), ground);
                    gridHandler[x - 1, y - 1] = Grid.FLOOR;
                    grounds.Add(new Vector2Int(x - 1, y - 1));
                }

                if (!GetIsFloor(x - 1, y + 1) && !GetIsFloor(x + 1, y - 1))
                {
                    //tilemap.SetTile(new Vector3Int(x + 1 + (int)roomOffset.x, y - 1 + (int)roomOffset.y), ground);
                    gridHandler[x + 1, y - 1] = Grid.FLOOR;
                    grounds.Add(new Vector2Int(x + 1, y - 1));
                }
            }
        }
        FloorGenerator.instance.roomProcessesFinished++;

        yield return null;

    }

    private bool GetIsFloor(int x, int y)
    {
        return gridHandler[x, y] == Grid.FLOOR;
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
}
