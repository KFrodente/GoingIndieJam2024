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
        WALL,
        EMPTY
    }

    public enum Type
    {
        BASIC,
        TREASURE,
        SHOP,
        BOSS
    }

    public Tilemap tilemap = null;

    public bool connectsUp;
    public Room roomConnectedUp = null;
    public bool connectsRight;
    public Room roomConnectedRight = null;
    public bool connectsDown;
    public Room roomConnectedDown = null;
    public bool connectsLeft;
    public Room roomConnectedLeft = null;

    public Type roomType;

    public bool activeRoom = false;
}
