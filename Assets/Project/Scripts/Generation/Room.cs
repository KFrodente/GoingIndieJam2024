using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    protected enum Grid
    {
        FLOOR,
        PFLOOR,
        WALL,
        EMPTY
    }

    public enum Type
    {
        BASIC,
        SPAWN,
        TREASURE,
        SHOP,
        BOSS
    }

    public Tilemap tilemap = null;

    [SerializeField] protected GameObject basicPortal;
    [SerializeField] protected GameObject shopPortal;
    [SerializeField] protected GameObject treasurePortal;
    [SerializeField] protected GameObject bossPortal;


    public bool connectsUp;
    public Room roomConnectedUp = null;
    public Portal topPortal;

    public bool connectsRight;
    public Room roomConnectedRight = null;
    public Portal rightPortal;

    public bool connectsDown;
    public Room roomConnectedDown = null;
    public Portal bottomPortal;

    public bool connectsLeft;
    public Room roomConnectedLeft = null;
    public Portal leftPortal;

    public Type roomType;

    public bool activeRoom = false;

    public virtual void GeneratePortals()
    {

    }

    //Returns spawned portal
    protected GameObject GenerateRespectivePortal(Room connectedRoom, Transform portalPos)
    {
        GameObject go;
        switch (connectedRoom.roomType)
        {
            case Type.BASIC:
            case Type.SPAWN:
                go = Instantiate(basicPortal, portalPos.position, portalPos.rotation);
                go.GetComponent<Portal>().connectedRoom = connectedRoom;
                return go;
            case Type.TREASURE:
                go = Instantiate(treasurePortal, portalPos.position, portalPos.rotation);
                Vector2Int costToEnter = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum].costToEnter;
                go.GetComponent<Portal>().costToEnter = Random.Range(costToEnter.x, costToEnter.y);
                go.GetComponent<Portal>().connectedRoom = connectedRoom;
                return go;
            case Type.SHOP:
                go = Instantiate(shopPortal, portalPos.position, portalPos.rotation);
                go.GetComponent<Portal>().connectedRoom = connectedRoom;
                return go;
            case Type.BOSS:
                go = Instantiate(bossPortal, portalPos.position, portalPos.rotation);
                go.GetComponent<Portal>().connectedRoom = connectedRoom;
                return go;
        }
        return null;
    }
}
