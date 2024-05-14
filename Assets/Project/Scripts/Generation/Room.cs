using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    protected enum Grid
    {
        ROCK,
        DIRT,
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


    [HideInInspector] public bool connectsUp;
    [HideInInspector] public Room roomConnectedUp = null;
    [HideInInspector] public Portal topPortal;

    [HideInInspector] public bool connectsRight;
    [HideInInspector] public Room roomConnectedRight = null;
    [HideInInspector] public Portal rightPortal;

    [HideInInspector] public bool connectsDown;
    [HideInInspector] public Room roomConnectedDown = null;
    [HideInInspector] public Portal bottomPortal;

    [HideInInspector] public bool connectsLeft;
    [HideInInspector] public Room roomConnectedLeft = null;
    [HideInInspector] public Portal leftPortal;

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
        switch (connectedRoom.roomType)
        {
            case Type.BASIC:
            case Type.SPAWN:
                go = Instantiate(basicPortal, new Vector3(portalPos.x, portalPos.y, 0), transform.rotation, transform);
                return go;
            case Type.TREASURE:
                go = Instantiate(treasurePortal, new Vector3(portalPos.x, portalPos.y, 0), transform.rotation, transform);
                
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
