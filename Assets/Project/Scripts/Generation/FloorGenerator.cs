using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

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

        CreateSpecialRooms(bossRooms[Random.Range(0, bossRooms.Count)], 'B', floorStats[floorNum].minBossDistance, Room.Type.BOSS);

        for (int i = 0; i < floorStats[floorNum].treasureRoomAmount; i++)
        {
            CreateSpecialRooms(treasureRooms[Random.Range(0, treasureRooms.Count)], 't', floorStats[floorNum].minTreasureDistance, Room.Type.TREASURE);
        }

        for (int i = 0; i < floorStats[floorNum].shopRoomAmount; i++)
        {
            CreateSpecialRooms(shopRooms[Random.Range(0, shopRooms.Count)], 'S', floorStats[floorNum].minShopDistance, Room.Type.SHOP);
        }

        

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

    private void CreateSpecialRooms(GameObject room, char letter, int minPlaceDistance, Room.Type type)
    {
        List<Vector2> usablePositions = new();
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms.ElementAt(i).Value == 'b')
            {
                Vector2 currentRoom = roomObjectDictionary.ElementAt(i).Key;
                if ((currentRoom + Vector2.up).y >= minPlaceDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.up))
                {
                    usablePositions.Add(currentRoom + Vector2.up);
                }
                if ((currentRoom + Vector2.right).x >= minPlaceDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.right))
                {
                    usablePositions.Add(currentRoom + Vector2.right);
                }
                if ((currentRoom + Vector2.down).x >= minPlaceDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.down))
                {
                    usablePositions.Add(currentRoom + Vector2.down);
                }
                if ((currentRoom + Vector2.left).x >= minPlaceDistance && !roomObjectDictionary.ContainsKey(currentRoom + Vector2.left))
                {
                    usablePositions.Add(currentRoom + Vector2.left);
                }
            }
        }

        Vector2 pickedPos = usablePositions[Random.Range(0, usablePositions.Count)];

        GameObject br = Instantiate(room, new Vector3(floorStats[floorNum].roomOffset.x * pickedPos.x + (floorStats[floorNum].roomOffset.x / 2), floorStats[floorNum].roomOffset.y * pickedPos.y + (floorStats[floorNum].roomOffset.y / 2), 0), transform.rotation);

        br.GetComponent<Room>().roomType = type;

        Room checkedRoom;


        if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.up, out checkedRoom))
        {
            checkedRoom.connectsDown = true;
            checkedRoom.roomConnectedDown = br.GetComponent<Room>();
            br.GetComponent<Room>().connectsUp = true;
            br.GetComponent<Room>().roomConnectedUp = checkedRoom;
        }
        else if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.down, out checkedRoom))
        {
            checkedRoom.connectsUp = true;
            checkedRoom.roomConnectedUp = br.GetComponent<Room>();
            br.GetComponent<Room>().connectsDown = true;
            br.GetComponent<Room>().roomConnectedDown = checkedRoom;
        }
        else if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.right, out checkedRoom))
        {
            checkedRoom.connectsLeft = true;
            checkedRoom.roomConnectedLeft = br.GetComponent<Room>();
            br.GetComponent<Room>().connectsRight = true;
            br.GetComponent<Room>().roomConnectedRight = checkedRoom;
        }
        else if (roomObjectDictionary.TryGetValue(pickedPos + Vector2.left, out checkedRoom))
        {
            checkedRoom.connectsRight = true;
            checkedRoom.roomConnectedRight = br.GetComponent<Room>();
            br.GetComponent<Room>().connectsLeft = true;
            br.GetComponent<Room>().roomConnectedLeft = checkedRoom;
        }

        rooms.Add(pickedPos, letter);
        roomObjectDictionary.Add(pickedPos, br.GetComponent<Room>());
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
