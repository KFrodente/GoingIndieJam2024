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

    private int floorNum = 0;

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
        SetSpawn();
        BuildBasicRooms();
        //SetBossRoom();
        
        for (int i = 0; i < rooms.Count; i++)
        {
            Debug.Log($"{rooms.ElementAt(i).Key}: {rooms.ElementAt(i).Value}");
        }

        floorNum++;
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

        if (neighboringRooms >= maxNeighboringRooms && Random.Range(0f, 1f) > ruleBreakChance) return false;

        return true;

    }

    private void CreateRoom(Vector2 pos, Vector2 direction)
    {
        WalkerGenerator newGen = Instantiate(basicRoomForFloor[floorNum], transform.position, transform.rotation);
        newGen.gridXOffset *= (int)pos.x + (int)direction.x;
        newGen.gridYOffset *= (int)pos.y + (int)direction.y;

        if (direction == Vector2.up) newGen.connectsDown = true;
        if (direction == Vector2.right) newGen.connectsLeft = true;
        if (direction == Vector2.down) newGen.connectsUp = true;
        if (direction == Vector2.left) newGen.connectsRight = true;
    }
    #endregion

    #region Boss Room Section

    private void SetBossRoom()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            //Vector2 currentPos = 
        }
    }

    #endregion
}
