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

    [Header("Rooms")]
    [SerializeField] private List<WalkerGenerator> basicRoomForFloor = new();
    [SerializeField] private GameObject spawnRoom;
    [SerializeField] private GameObject bossRoom;
    [SerializeField] private GameObject treasureRoom;

    public List<FloorStatsSO> floorStats = new();

    [SerializeField, Range(0, 4)] private int maxNeighboringRooms;
    [SerializeField, Range(0, 1)] private float ruleBreakChance;

    private int basicRooms;
    private int treasureRooms;
    private int shopRooms;

    [SerializeField, Range(0, 1000)] private int basicRoomAmount;
    [SerializeField, Range(0, 100)] private int treasureRoomAmount;
    [SerializeField, Range(0, 100)] private int shopRoomAmount;
    [SerializeField, Range(0, 100)] private int minBossRoomDistance;

    private Dictionary<Vector2, char> rooms = new();
    private Dictionary<Vector2, Room> roomObjectDictionary = new();

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
        SetTotalProcesses();
        SetSpawn();
        BuildBasicRooms();
        ConnectBasicRooms();
        SetBossRoom();
        //SpawnTreasureRooms();
        //SpawnShopRooms();
    }

    private void SetTotalProcesses()
    {
        totalFloorProcesses += basicRoomAmount * WalkerGenerator.basicRoomProcesses;
    }

    private void SetSpawn()
    {
        rooms.Add(new Vector2(0, 0), 's');
        GameObject spawn = Instantiate(spawnRoom, new Vector3((int)floorStats[floorNum].roomWidth / 2, (int)floorStats[floorNum].roomHeight / 2, 0), transform.rotation);
        roomObjectDictionary.Add(Vector2.zero, spawn.GetComponent<Spawn>());
    }

    #region Basic Room section
    private void BuildBasicRooms()
    {
        while (basicRooms < basicRoomAmount)
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
                        CreateRoom(pos, Vector2.down);
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

        roomObjectDictionary.Add(pos + direction, newGen);
    }

    private void ConnectBasicRooms()
    {
        for (int i = 0; i < roomObjectDictionary.Count; i++)
        {
            Vector2 currentKey = roomObjectDictionary.ElementAt(i).Key;
            if (roomObjectDictionary.ContainsKey(currentKey + Vector2.up))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsUp = true;
            }
            if (roomObjectDictionary.ContainsKey(currentKey + Vector2.right))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsRight = true;
            }
            if (roomObjectDictionary.ContainsKey(currentKey + Vector2.down))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsDown = true;
            }
            if (roomObjectDictionary.ContainsKey(currentKey + Vector2.left))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsLeft = true;
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
            Vector2 currentRoom = roomObjectDictionary.ElementAt(i).Key;
            if ((currentRoom + Vector2.up).y >= minBossRoomDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.up))
            {
                usablePositions.Add(currentRoom + Vector2.up);
            }
            if ((currentRoom + Vector2.right).x >= minBossRoomDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.down))
            {
                usablePositions.Add(currentRoom + Vector2.right);
            }
            if ((currentRoom + Vector2.down).x >= minBossRoomDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.right))
            {
                usablePositions.Add(currentRoom + Vector2.down);
            }
            if ((currentRoom + Vector2.left).x >= minBossRoomDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.left))
            {
                usablePositions.Add(currentRoom + Vector2.left);
            }
        }

        Vector2 pickedPos = usablePositions[UnityEngine.Random.Range(0, usablePositions.Count)];

        GameObject br = Instantiate(bossRoom, new Vector3(floorStats[floorNum].roomOffset.x * pickedPos.x + (floorStats[floorNum].roomOffset.x / 2), floorStats[floorNum].roomOffset.y * pickedPos.y + (floorStats[floorNum].roomOffset.y / 2), 0), transform.rotation);

        Room checkedRoom;

        List<Room> possibleRooms = new();

        if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.up, out checkedRoom))
        {
            checkedRoom.connectsDown = true;
            br.GetComponent<Room>().connectsUp = true;
        }
        else if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.down, out checkedRoom))
        {
            checkedRoom.connectsUp = true;
            br.GetComponent<Room>().connectsDown = true;
        }
        else if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.right, out checkedRoom))
        {
            checkedRoom.connectsLeft = true;
            br.GetComponent<Room>().connectsRight = true;
        }
        else if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.left, out checkedRoom))
        {
            checkedRoom.connectsRight = true;
            br.GetComponent<Room>().connectsLeft = true;
        }

        rooms.Add(pickedPos, 'B');
        roomObjectDictionary.Add(pickedPos, br.GetComponent<Room>());
    }

    #endregion

    #region Treasure Room Section
    //private void SpawnTreasureRooms()
    //{
    //    int tRoomsToSpawn = treasureRoomAmount;
    //    List<Vector2> pos = new(); 
    //    List<Vector2> dir = new();

    //    GetPosAndDirs();

    //    while (tRoomsToSpawn > 0)
    //    {
    //        int index = UnityEngine.Random.Range(0, posAndDir.Count);

    //        GameObject tRoom = Instantiate(treasureRoom, new Vector3(floorStats[floorNum].roomOffset.x * (posAndDir.ElementAt(index).Key.x + posAndDir.ElementAt(index).Value.x) + (floorStats[floorNum].roomOffset.x / 2), floorStats[floorNum].roomOffset.y * (posAndDir.ElementAt(index).Key.y + posAndDir.ElementAt(index).Value.y) + (floorStats[floorNum].roomOffset.y / 2), 0), transform.rotation);

    //        Room room;

    //        if (posAndDir.ElementAt(index).Value == Vector2.up)
    //        {
    //            tRoom.GetComponent<Room>().connectsDown = true;
    //            roomObjectDictionary.TryGetValue(posAndDir.ElementAt(index).Key, out room );
    //            room.connectsUp = true;
    //        }
    //        else if (posAndDir.ElementAt(index).Value == Vector2.right)
    //        {
    //            tRoom.GetComponent<Room>().connectsLeft = true;
    //            roomObjectDictionary.TryGetValue(posAndDir.ElementAt(index).Key, out room);
    //            room.connectsRight = true;
    //        }
    //        else if (posAndDir.ElementAt(index).Value == Vector2.left)
    //        {
    //            tRoom.GetComponent<Room>().connectsRight = true;
    //            roomObjectDictionary.TryGetValue(posAndDir.ElementAt(index).Key, out room);
    //            room.connectsLeft = true;
    //        }
    //        else if (posAndDir.ElementAt(index).Value == Vector2.down)
    //        {
    //            tRoom.GetComponent<Room>().connectsUp = true;
    //            roomObjectDictionary.TryGetValue(posAndDir.ElementAt(index).Key, out room);
    //            tRoom.transform.position = new Vector3(floorStats[floorNum].roomOffset.x * posAndDir.ElementAt(index).Key.x + (floorStats[floorNum].roomOffset.x / 2), floorStats[floorNum].roomOffset.y * posAndDir.ElementAt(index).Key.y + (floorStats[floorNum].roomOffset.y / 2), 0);
    //            room.connectsDown = true;
    //        }
    //        tRoomsToSpawn--;
    //    }
    //}


    //private Dictionary<Vector2, Vector2> GetPosAndDirs()
    //{
    //    Dictionary<Vector2, Vector2> returnDict = new();
    //    for (int i = 0; i < rooms.Count; i++)
    //    {
    //        Vector2 currentRoom = roomObjectDictionary.ElementAt(i).Key;
    //        if (!roomObjectDictionary.ContainsKey(currentRoom + Vector2.up))
    //        {
    //            returnDict.TryAdd(currentRoom, Vector2.up);
    //        }
    //        if (!roomObjectDictionary.ContainsKey(currentRoom + Vector2.down))
    //        {
    //            returnDict.TryAdd(currentRoom, Vector2.down);
    //        }
    //        if (!roomObjectDictionary.ContainsKey(currentRoom + Vector2.right))
    //        {
    //            returnDict.TryAdd(currentRoom, Vector2.right);
    //        }
    //        if (!roomObjectDictionary.ContainsKey(currentRoom + Vector2.left))
    //        {
    //            returnDict.TryAdd(currentRoom, Vector2.left);
    //        }
    //    }
    //    return returnDict;

    //}
    #endregion
}
