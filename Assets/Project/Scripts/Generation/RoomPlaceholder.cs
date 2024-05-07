using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlaceholder
{
    public enum RoomType
    { 
        BASIC,
        SPAWN,
        TREASURE,
        SHOP,
        BOSS
    }

    public RoomPlaceholder(Vector2 position, RoomType roomType)
    {
        this.roomPosition = position;
        this.roomType = roomType;
    }

    public Vector2 roomPosition;

    public RoomType roomType;

    public int connectedRooms;

    public bool connectsRight = false;
    public bool connectsLeft = false;
    public bool connectsUp = false;
    public bool connectsDown = false;

    //returns selected position
    public Vector2 GetRandomDirection()
    {
        if (connectedRooms > 1) return Vector2.zero;
        List<int> directions = new();


        if (!connectsRight) directions.Add(0);
        if (!connectsLeft) directions.Add(1);
        if (!connectsUp) directions.Add(2);
        if (!connectsDown) directions.Add(3);

        int pickedDir = Random.Range(0, directions.Count);

        if (directions[pickedDir] == 0)
        {
            connectsRight = true;
            connectedRooms++;
            return Vector2.right;
        }
        if (directions[pickedDir] == 1)
        {
            connectsLeft = true;
            connectedRooms++;
            return Vector2.left;
        }
        if (directions[pickedDir] == 2)
        {
            connectsUp = true;
            connectedRooms++;
            return Vector2.up;
        }
        if (directions[pickedDir] == 3)
        {
            connectsDown = true;
            connectedRooms++;
            return Vector2.down;
        }

        return Vector2.zero;
    }
}
