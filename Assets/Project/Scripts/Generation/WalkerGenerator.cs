using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
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

    [SerializeField] private Grid[,] gridHandler;
    [SerializeField] private List<WalkerObject> walkers;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile floor;
    [SerializeField] private RuleTile wall;
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;

    [SerializeField] private int maxWalkers;
    [SerializeField] private int tileCount;
    [SerializeField, Range(0, 1)] private float fillPercentage = .03f;

    [SerializeField] private int timesToThicken;
    [SerializeField, Range(0, 1)] private float chanceToThicken;

    [SerializeField] private int maxFailedRedirects;
    [SerializeField, Range(0, 1)] private float redirectChance;
    [SerializeField, Range(0, 1)] private float removeChance;
    [SerializeField, Range(0, 1)] private float createChance;

    //[SerializeField] private int maxContinuousLine;

    private int walkersPlaced;

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

        Vector2 pickedDir = GetDirection();
        WalkerObject currentWalker = new WalkerObject(new Vector2(tileCenter.x, tileCenter.y), pickedDir, removeChance, redirectChance, createChance, walkersPlaced);
        walkersPlaced++;
        currentWalker.prevDirections.Add(pickedDir);
        gridHandler[tileCenter.x, tileCenter.y] = Grid.FLOOR;
        tilemap.SetTile(tileCenter, floor);
        walkers.Add(currentWalker);

        tileCount++;

        CreateFloors();
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

    private void CreateFloors()
    {
        while (tileCount / (float)gridHandler.Length < fillPercentage)
        {

            foreach (WalkerObject walker in walkers)
            {
                Vector3Int pos = new Vector3Int((int)walker.position.x, (int)walker.position.y, 0);

                if (gridHandler[pos.x, pos.y] != Grid.FLOOR)
                {
                    tilemap.SetTile(pos, floor);
                    tileCount++;
                    gridHandler[pos.x, pos.y] = Grid.FLOOR;
                }
            }

            CheckRemove();
            CheckRedirect();
            CheckCreate();
            UpdatePosition();
        }

        for (int i = 0; i < timesToThicken; i++)
        {

            CreateWalls();
        }
        Bulk();

    }

    private void CreateWalls()
    {
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (x >= 1 && x < gridHandler.GetLength(0) && y >= 1 && y < gridHandler.GetLength(1))
                {
                    if (gridHandler[x+1, y] == Grid.FLOOR && gridHandler[x, y+1] == Grid.FLOOR && Random.Range(0.0f, 1.0f) < chanceToThicken)
                    {
                        tilemap.SetTile(new Vector3Int(x, y), floor);
                        gridHandler[x, y] = Grid.FLOOR;
                    }
                    else if (gridHandler[x + 1, y] == Grid.FLOOR && gridHandler[x, y - 1] == Grid.FLOOR && Random.Range(0.0f, 1.0f) < chanceToThicken)
                    {
                        tilemap.SetTile(new Vector3Int(x, y), floor);
                        gridHandler[x, y] = Grid.FLOOR;
                    }
                    else if (gridHandler[x - 1, y] == Grid.FLOOR && gridHandler[x, y + 1] == Grid.FLOOR && Random.Range(0.0f, 1.0f) < chanceToThicken)
                    {
                        tilemap.SetTile(new Vector3Int(x, y), floor);
                        gridHandler[x, y] = Grid.FLOOR;
                    }
                    else if (gridHandler[x, y] == Grid.EMPTY)
                    {
                        tilemap.SetTile(new Vector3Int(x, y), wall);
                        gridHandler[x, y] = Grid.WALL;
                    }
                }
                if (gridHandler[x, y] == Grid.EMPTY)
                {
                    tilemap.SetTile(new Vector3Int(x, y), wall);
                    gridHandler[x, y] = Grid.WALL;

                }
            }
        }
    }

    private void Bulk()
    {
        for (int x = 0; x < gridHandler.GetLength(0) - 1; x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1) - 1; y++)
            {
                if (gridHandler[x, y] == Grid.FLOOR)
                {
                    if (!GetIsFloor(x, y + 1) && !GetIsFloor(x, y - 1))
                    {
                        tilemap.SetTile(new Vector3Int(x, y - 1), floor);
                        gridHandler[x, y - 1] = Grid.FLOOR;
                    }
                    else if (!GetIsFloor(x + 1, y) && !GetIsFloor(x - 1, y))
                    {
                        tilemap.SetTile(new Vector3Int(x - 1, y), floor);
                        gridHandler[x - 1, y] = Grid.FLOOR;
                    }
                }
            }
        }

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
            if (Random.Range(0f, 1f) < walker.chanceToRedirect || walker.failedRedirects >= maxFailedRedirects)
            {
                Vector2 pickedDir = GetDirection();
                walker.prevDirections.Add(pickedDir);
                walker.direction = pickedDir;
            }
            else
            {
                walker.failedRedirects++;
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

                WalkerObject newWalker = new WalkerObject(newPos, newDir, walkers[i].chanceToRemove, walkers[i].chanceToRedirect, walkers[i].chanceToCreate, walkersPlaced);
                walkersPlaced++;
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
