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

    public void ConnectPortals()
    {
        if (connectsUp) topPortal.connectedPortal = roomConnectedUp.bottomPortal;
        if (connectsRight) rightPortal.connectedPortal = roomConnectedRight.leftPortal;
        if (connectsLeft) leftPortal.connectedPortal = roomConnectedLeft.rightPortal;
        if (connectsDown) bottomPortal.connectedPortal = roomConnectedDown.topPortal;
    }

    //Returns spawned portal
    protected GameObject GenerateRespectivePortal(Room connectedRoom, Vector2 portalPos)
    {
        GameObject go;
        Debug.Log("made portal");
        switch (connectedRoom.roomType)
        {
            case Type.BASIC:
            case Type.SPAWN:
                go = Instantiate(basicPortal, new Vector3(portalPos.x, portalPos.y, 0), transform.rotation, transform);
                return go;
            case Type.TREASURE:
                go = Instantiate(treasurePortal, new Vector3(portalPos.x, portalPos.y, 0), transform.rotation, transform);
                Vector2Int costToEnter = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum].costToEnter;
                go.GetComponent<Portal>().costToEnter = Random.Range(costToEnter.x, costToEnter.y);
                return go;
            case Type.SHOP:
                go = Instantiate(shopPortal, new Vector3(portalPos.x, portalPos.y, 0), transform.rotation, transform);
                return go;
            case Type.BOSS:
                go = Instantiate(bossPortal, new Vector3(portalPos.x, portalPos.y, 0), transform.rotation, transform);
                return go;
        }
        return null;
    }
}
