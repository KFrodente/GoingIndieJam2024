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

    public Tilemap tilemap = null;

    public bool connectsUp;
    public bool connectsRight;
    public bool connectsDown;
    public bool connectsLeft;

    public bool activeRoom = false;
}
