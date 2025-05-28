using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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

    public GameObject corvid;
    public GameObject dauntless;
    public GameObject tethered;

    [Header("Spawnable Rooms")]
    [SerializeField] private List<GameObject> basicRoomForFloor = new();
    [SerializeField] private List<GameObject> spawnRooms = new();
    [SerializeField] private List<GameObject> bossRooms = new();
    [SerializeField] private List<GameObject> treasureRooms = new();
    [SerializeField] private List<GameObject> shopRooms = new();

    [Header("Floor Stats")]
    public List<FloorStatsSO> floorStats = new();

    [SerializeField, Range(0, 4)] private int maxNeighboringRooms;

    private int placedBasicRooms;

    private Dictionary<Vector2, char> rooms = new();
    private Dictionary<Vector2, Room> roomObjectDictionary = new();

    private int totalFloorProcesses = 0;
    public int roomProcessesFinished = 0;

    public int floorNum = 0;


    private FloorFacade floorFacade;

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

        MakeFancyRooms();
        
        StartCoroutine(DoPortals());

    }

    private IEnumerator DoPortals()
    {
        yield return new WaitForSeconds(.1f);
        GeneratePortals();
        yield return new WaitForSeconds(.1f);

        //SpiritCharacter.souls = 100;

        for (int i = 0; i < roomObjectDictionary.Count; i++)
        {
            roomObjectDictionary.ElementAt(i).Value.ConnectPortals();
        }
    }

    private void SetTotalProcesses()
    {
        totalFloorProcesses += floorStats[floorNum].basicRoomAmount * WalkerGenerator.basicRoomProcesses;
    }

    
    private void SetSpawn()
    {
        rooms.Add(new Vector2(0, 0), 's');
        GameObject spawn = Instantiate(spawnRooms[Random.Range(0, spawnRooms.Count)], new Vector3((int)floorStats[floorNum].roomWidth / 2, (int)floorStats[floorNum].roomHeight / 2, 0), transform.rotation);
        roomObjectDictionary.Add(Vector2.zero, spawn.GetComponent<Spawn>());
    }

    #region Basic Room section
    private void BuildBasicRooms()
    {
        while (placedBasicRooms < floorStats[floorNum].basicRoomAmount)
        {
            int pickedPos = Random.Range(0, rooms.Count);

            Vector2 pos = rooms.ElementAt(pickedPos).Key;

            int pickedDir = Random.Range(0, 4);
            switch (pickedDir)
            {
                case 0:
                    if (CanUseSelected(pos + Vector2.right))
                    {
                        rooms.Add(new Vector2(pos.x + 1, pos.y), 'b');
                        CreateRoom(pos, Vector2.right);

                        placedBasicRooms++;
                    }
                    break;
                case 1:
                    if (CanUseSelected(pos + Vector2.up))
                    {
                        rooms.Add(new Vector2(pos.x, pos.y + 1), 'b');
                        CreateRoom(pos, Vector2.up);
                        placedBasicRooms++;
                    }
                    break;
                case 2:

                    if (CanUseSelected(pos + Vector2.left))
                    {
                        rooms.Add(new Vector2(pos.x - 1, pos.y), 'b');
                        CreateRoom(pos, Vector2.left);
                        placedBasicRooms++;
                    }
                    break;
                case 3:
                    if (CanUseSelected(pos + Vector2.down))
                    {
                        rooms.Add(new Vector2(pos.x, pos.y - 1), 'b');
                        CreateRoom(pos, Vector2.down);
                        placedBasicRooms++;
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
        GameObject go = Instantiate(original: basicRoomForFloor[floorNum], transform.position, transform.rotation);
        WalkerGenerator newGen = go.GetComponent<WalkerGenerator>();
        newGen.roomOffset = new Vector2(floorStats[floorNum].roomOffset.x * ((int)pos.x + (int)direction.x), floorStats[floorNum].roomOffset.x * ((int)pos.y + (int)direction.y));

        roomObjectDictionary.Add(pos + direction, newGen);
    }

    private void ConnectBasicRooms()
    {
        for (int i = 0; i < roomObjectDictionary.Count; i++)
        {
            Vector2 currentKey = roomObjectDictionary.ElementAt(i).Key;

            Room rm;

            if (roomObjectDictionary.TryGetValue(currentKey + Vector2.up, out rm))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsUp = true;
                roomObjectDictionary.ElementAt(i).Value.roomConnectedUp = rm;
            }
            if (roomObjectDictionary.TryGetValue(currentKey + Vector2.right, out rm))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsRight = true;
                roomObjectDictionary.ElementAt(i).Value.roomConnectedRight = rm;
            }
            if (roomObjectDictionary.TryGetValue(currentKey + Vector2.down, out rm))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsDown = true;
                roomObjectDictionary.ElementAt(i).Value.roomConnectedDown = rm;
            }
            if (roomObjectDictionary.TryGetValue(currentKey + Vector2.left, out rm))
            {
                roomObjectDictionary.ElementAt(i).Value.connectsLeft = true;
                roomObjectDictionary.ElementAt(i).Value.roomConnectedLeft = rm;
            }
        }
    }

    #endregion

    public void MakeFancyRooms(GameObject bossRoom = null, GameObject treasureRoom = null, GameObject shopRoom = null)
    {
        MakeBossRoom();
        MakeTreasureRooms();
        MakeShopRooms();
    }

    public void MakeBossRoom(GameObject room = null)
    {
        GameObject selectedRoom = (room == null) ? SelectRoom(bossRooms) : room;
        int minDist = floorStats[floorNum].minTreasureDistance;
        CreateSpecialRooms(selectedRoom, 'B', floorStats[floorNum].minBossDistance, Room.Type.BOSS);
    }

    public void MakeTreasureRooms(GameObject room = null)
    {
        for (int i = 0; i < floorStats[floorNum].treasureRoomAmount; i++)
        {
            GameObject selectedRoom = (room == null) ? SelectRoom(treasureRooms) : room;
            int minDist = floorStats[floorNum].minTreasureDistance;
            CreateSpecialRooms(selectedRoom, 't', minDist, Room.Type.TREASURE);
        }
    }

    public void MakeShopRooms(GameObject room = null)
    {
        for (int i = 0; i < floorStats[floorNum].shopRoomAmount; i++)
        {
            GameObject selectedRoom = (room == null) ? SelectRoom(shopRooms) : room;
            int minDist = floorStats[floorNum].minShopDistance;
            CreateSpecialRooms(selectedRoom, 'S', minDist, Room.Type.SHOP);
        }
    }

    private GameObject SelectRoom(List<GameObject> roomList)
    {
        return roomList[Random.Range(0, roomList.Count)];
    }

    private void CreateSpecialRooms(GameObject room, char letter, int minPlaceDistance, Room.Type type)
    {

        List<Vector2> usablePositions = new();
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms.ElementAt(i).Value == 'b')
            {
                Vector2 currentRoom = roomObjectDictionary.ElementAt(i).Key;
                if (CheckUsableRoom(currentRoom, Vector2.up, minPlaceDistance))
                {
                    usablePositions.Add(currentRoom + Vector2.up);
                }
                if (CheckUsableRoom(currentRoom, Vector2.right, minPlaceDistance))
                {
                    usablePositions.Add(currentRoom + Vector2.right);
                }
                if (CheckUsableRoom(currentRoom, Vector2.down, minPlaceDistance))
                {
                    usablePositions.Add(currentRoom + Vector2.down);
                }
                if (CheckUsableRoom(currentRoom, Vector2.left, minPlaceDistance))
                {
                    usablePositions.Add(currentRoom + Vector2.left);
                }
            }
        }

        Vector2 pickedPos = usablePositions[Random.Range(0, usablePositions.Count)];

        GameObject br = Instantiate(room, new Vector3(floorStats[floorNum].roomOffset.x * pickedPos.x + (floorStats[floorNum].roomOffset.x / 2), floorStats[floorNum].roomOffset.y * pickedPos.y + (floorStats[floorNum].roomOffset.y / 2), 0), transform.rotation);


        Room brRoom = br.GetComponent<Room>();

        brRoom.roomType = type;


        //Room checkedRoom;

        //char checkedChar;

        FloorFacade connectionFacade = new FloorFacade(rooms, roomObjectDictionary);
        connectionFacade.ConnectRoomsAround(pickedPos, brRoom);



        //if (rooms.TryGetValue(pickedPos + Vector2.up, out checkedChar))
        //{
        //    if (checkedChar == 'b' || checkedChar == 's')
        //    {
        //        if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.up, out checkedRoom))
        //        {
        //            checkedRoom.connectsDown = true;
        //            checkedRoom.roomConnectedDown = brRoom;
        //            brRoom.connectsUp = true;
        //            brRoom.roomConnectedUp = checkedRoom;
        //        }
        //    }
        //}
        //else if (rooms.TryGetValue(pickedPos + Vector2.down, out checkedChar))
        //{
        //    if (checkedChar == 'b' || checkedChar == 's')
        //    {
        //        if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.down, out checkedRoom))
        //        {
        //            checkedRoom.connectsUp = true;
        //            checkedRoom.roomConnectedUp = brRoom;
        //            brRoom.connectsDown = true;
        //            brRoom.roomConnectedDown = checkedRoom;
        //        }
        //    }
        //}
        //else if (rooms.TryGetValue(pickedPos + Vector2.right, out checkedChar))
        //{
        //    if (checkedChar == 'b' || checkedChar == 's')
        //    {
        //        if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.right, out checkedRoom))
        //        {
        //            checkedRoom.connectsLeft = true;
        //            checkedRoom.roomConnectedLeft = brRoom;
        //            brRoom.connectsRight = true;
        //            brRoom.roomConnectedRight = checkedRoom;
        //        }
        //    }
        //}
        //else if (rooms.TryGetValue(pickedPos + Vector2.left, out checkedChar))
        //{
        //    if (checkedChar == 'b' || checkedChar == 's')
        //    {
        //        if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.left, out checkedRoom))
        //        {
        //            checkedRoom.connectsRight = true;
        //            checkedRoom.roomConnectedRight = brRoom;
        //            brRoom.connectsLeft = true;
        //            brRoom.roomConnectedLeft = checkedRoom;
        //        }
        //    }
        //}

        rooms.Add(pickedPos, letter);
        roomObjectDictionary.Add(pickedPos, br.GetComponent<Room>());
    }

    private bool CheckUsableRoom(Vector2 currentRoom, Vector2 dir, int minPlaceDist)
    {
        return (currentRoom + dir).y >= minPlaceDist && !roomObjectDictionary.ContainsKey(currentRoom + dir);
    }

    private void GeneratePortals()
    {
        for (int i = 0; i < roomObjectDictionary.Count; i++)
        {
            if (roomObjectDictionary.ElementAt(i).Value.roomType != Room.Type.BASIC)
            {
                roomObjectDictionary.ElementAt(i).Value.GeneratePortals();

            }
        }
    }
}
