using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
    {
        FLOOR,
        WALL,
        EMPTY
    }

    public Grid[,] gridHandler;
    public List<WalkerObject> walkers;
    public Tilemap tilemap;
    public Tile floor;
    public Tile wall;
    public int mapWidth;
    public int mapHeight;

    public int maxWalkers;
    public int tileCount;
    public float fillPercentage;
    public float waitTime;

    private void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        gridHandler = new Grid[mapWidth, mapHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x, y] = Grid.EMPTY;
            }
        }

        walkers = new List<WalkerObject>();

        Vector3Int tileCenter = new Vector3Int(gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2, 0);

        WalkerObject currentWalker = new WalkerObject(new Vector2(tileCenter.x, tileCenter.y), GetDirection(), 0.8f, .99f, 0.6f);
        gridHandler[tileCenter.x, tileCenter.y] = Grid.FLOOR;
        tilemap.SetTile(tileCenter, floor);
        walkers.Add(currentWalker);

        tileCount++;

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
        while ((float)tileCount / (float)gridHandler.Length < fillPercentage)
        {

            bool hasCreatedFloor = false;
            foreach (WalkerObject walker in walkers)
            {
                Vector3Int pos = new Vector3Int((int)walker.position.x, (int)walker.position.y, 0);

                if (gridHandler[pos.x, pos.y] != Grid.FLOOR)
                {
                    tilemap.SetTile(pos, floor);
                    tileCount++;
                    gridHandler[pos.x, pos.y] = Grid.FLOOR;
                    hasCreatedFloor = true;
                }
            }

            CheckRemove();
            CheckRedirect();
            CheckCreate();
            UpdatePosition();

            if (hasCreatedFloor)
            {
                yield return new WaitForSeconds(waitTime);
            }
        }

        StartCoroutine(CreateWalls());

    }

    private IEnumerator CreateWalls()
    {
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (gridHandler[x, y] == Grid.FLOOR)
                {
                    bool hasCreatedWall = false;

                    if (gridHandler[x + 1, y] == Grid.EMPTY)
                    {
                        tilemap.SetTile(new Vector3Int(x + 1, y, 0), wall);
                        gridHandler[x + 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x - 1, y] == Grid.EMPTY)
                    {
                        tilemap.SetTile(new Vector3Int(x - 1, y, 0), wall);
                        gridHandler[x - 1, y] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y + 1] == Grid.EMPTY)
                    {
                        tilemap.SetTile(new Vector3Int(x, y + 1, 0), wall);
                        gridHandler[x, y + 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }
                    if (gridHandler[x, y - 1] == Grid.EMPTY)
                    {
                        tilemap.SetTile(new Vector3Int(x, y - 1, 0), wall);
                        gridHandler[x, y - 1] = Grid.WALL;
                        hasCreatedWall = true;
                    }

                    if (hasCreatedWall)
                    {
                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }
        }
    }

    private void CheckRemove()
    {
        int updatedCount = walkers.Count;
        for (int i = 0; i < updatedCount; i++)
        {
            if (Random.Range(0f, 1f) < walkers[i].chanceToRemove && walkers.Count > 1)
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
            if (Random.Range(0f, 1f) < walker.chanceToRedirect)
            {
                walker.direction = GetDirection();
            }
        }
    }

    private void CheckCreate()
    {
        int originalCount = walkers.Count;
        for (int i = 0; i < originalCount; i++)
        {
            if (Random.Range(0f, 1f) < walkers[i].chanceToCreate && walkers.Count < maxWalkers)
            {
                Vector2 newDir = GetDirection();
                Vector2 newPos = walkers[i].position;

                WalkerObject newWalker = new WalkerObject(newPos, newDir, walkers[i].chanceToRemove, walkers[i].chanceToRedirect, walkers[i].chanceToCreate);
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
