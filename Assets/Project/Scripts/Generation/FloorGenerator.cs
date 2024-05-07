using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class FloorGenerator : MonoBehaviour
{
    /*  
     *  ***** KEY *****
     *  s = spawn
     *  b = basic room
     *  S = shop room
     *  t = treasure room
     *  B = boss room
     */

    public static FloorGenerator instance;

    public Tilemap globalTilemap;

    [SerializeField] private List<WalkerGenerator> basicRoomForFloor = new();
    public List<FloorStatsSO> floorStats = new();

    [SerializeField, Range(0, 4)] private int maxNeighboringRooms;
    [SerializeField, Range(0, 1)] private float ruleBreakChance;

    private int basicRooms;
    private int treasureRooms;
    private int shopRooms;

    [SerializeField, Range(0, 100)] private int basicRoomAmount;
    [SerializeField, Range(0, 100)] private int treasureRoomAmount;
    [SerializeField, Range(0, 100)] private int shopRoomAmount;
    [SerializeField, Range(0, 100)] private int minBossRoomDistance;

    private Dictionary<Vector2, char> rooms = new();
    private Dictionary<Vector2, WalkerGenerator> basicRoomsDictionary = new();

    private int totalFloorProcesses = 0;

    public int roomProcessesFinished = 0;

    public int floorNum = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Start()
    {
        generateFloor();
    }

    public void generateFloor()
    {
        basicRoomsDictionary.Add(Vector2.zero, null);
        SetTotalProcesses();
        SetSpawn();
        BuildBasicRooms();
        ConnectBasicRooms();
        //SetBossRoom();
        
        for (int i = 0; i < rooms.Count; i++)
        {
            Debug.Log($"{rooms.ElementAt(i).Key}: {rooms.ElementAt(i).Value}");
            Debug.Log($"{basicRoomsDictionary.ElementAt(i).Key}");
        }
    }

    private void SetTotalProcesses()
    {
        totalFloorProcesses += basicRoomAmount * WalkerGenerator.basicRoomProcesses;
    }

    private void SetSpawn()
    {
        rooms.Add(new Vector2(0, 0), 's');
    }

    #region Basic Room section
    private void BuildBasicRooms()
    {
        while(basicRooms < basicRoomAmount)
        {
            int pickedPos = UnityEngine.Random.Range(0, rooms.Count);

            Vector2 pos = rooms.ElementAt(pickedPos).Key;

            int pickedDir = UnityEngine.Random.Range(0, 4);
            switch (pickedDir)
            { 
                case 0:
                    if (CanUseSelected(pos + Vector2.right))
                    {
                        rooms.Add(new Vector2(pos.x + 1, pos.y), 'b');
                        CreateRoom(pos, Vector2.right);
                        
                        basicRooms++;
                    }
                    break;
                case 1:
                    if (CanUseSelected(pos + Vector2.up))
                    {
                        rooms.Add(new Vector2(pos.x, pos.y + 1), 'b');
                        CreateRoom(pos, Vector2.up);
                        basicRooms++;
                    }
                    break;
                case 2:

                    if (CanUseSelected(pos + Vector2.left))
                    {
                        rooms.Add(new Vector2(pos.x - 1, pos.y), 'b');
                        CreateRoom(pos, Vector2.left);
                        basicRooms++;
                    }
                    break;
                case 3:
                    if (CanUseSelected(pos + Vector2.down))
                    {
                        rooms.Add(new Vector2(pos.x, pos.y - 1), 'b');
                        CreateRoom(pos, Vector2.down );
                        basicRooms++;
                    }
                    break;
                default:
                    Debug.LogWarning("Something went wrong in BuildBasicRooms() in the FloorGenerator class");
                    break;
            }
        }
    }

    private bool CanUseSelected(Vector2 pos)
    {
        if (rooms.ContainsKey(pos)) return false;

        int neighboringRooms = 0;
        if (rooms.ContainsKey(pos + Vector2.up)) neighboringRooms++;
        if (rooms.ContainsKey(pos + Vector2.right)) neighboringRooms++;
        if (rooms.ContainsKey(pos + Vector2.down)) neighboringRooms++;
        if (rooms.ContainsKey(pos + Vector2.left)) neighboringRooms++;

        if (neighboringRooms > maxNeighboringRooms) return false;

        return true;

    }

    private void CreateRoom(Vector2 pos, Vector2 direction)
    {
        WalkerGenerator newGen = Instantiate(original: basicRoomForFloor[floorNum], transform.position, transform.rotation);
        newGen.roomOffset = new Vector2(floorStats[floorNum].roomOffset.x * ((int)pos.x + (int)direction.x), floorStats[floorNum].roomOffset.x * ((int)pos.y + (int)direction.y));

        basicRoomsDictionary.Add(pos + direction, newGen);
    }

    private void ConnectBasicRooms()
    {
        for (int i = 1; i < basicRoomsDictionary.Count; i++)
        {
            Vector2 currentKey = basicRoomsDictionary.ElementAt(i).Key;
            if (basicRoomsDictionary.ContainsKey(currentKey + Vector2.up))
            {
                basicRoomsDictionary.ElementAt(i).Value.connectsUp = true;
            }
            if (basicRoomsDictionary.ContainsKey(currentKey + Vector2.right))
            {
                basicRoomsDictionary.ElementAt(i).Value.connectsRight = true;
            }
            if (basicRoomsDictionary.ContainsKey(currentKey + Vector2.down))
            {
                basicRoomsDictionary.ElementAt(i).Value.connectsDown = true;
            }
            if (basicRoomsDictionary.ContainsKey(currentKey + Vector2.left))
            {
                basicRoomsDictionary.ElementAt(i).Value.connectsLeft = true;
            }
        }
    }

    #endregion

    #region Boss Room Section

    private void SetBossRoom()
    {
        List<Vector2> usablePositions = new();
        for (int i = 0; i < rooms.Count; i++)
        {
            Vector2 currentRoom = rooms.ElementAt(i).Key;
            if ((currentRoom + Vector2.up).y >= minBossRoomDistance)
            {
                usablePositions.Add(currentRoom + Vector2.up);
            }
            if ((currentRoom + Vector2.right).x >= minBossRoomDistance)
            {
                usablePositions.Add(currentRoom + Vector2.right);
            }
            if ((currentRoom + Vector2.down).x >= minBossRoomDistance)
            {
                usablePositions.Add(currentRoom + Vector2.down);
            }
            if ((currentRoom + Vector2.left).x >= minBossRoomDistance)
            {
                usablePositions.Add(currentRoom + Vector2.left);
            }
        }

        int pickedPos = UnityEngine.Random.Range(0, usablePositions.Count);


    }

    #endregion
}
