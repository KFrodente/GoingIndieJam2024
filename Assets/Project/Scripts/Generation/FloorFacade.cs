using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFacade
{
    private Dictionary<Vector2, char> rooms;
    private Dictionary<Vector2, Room> roomObjectDictionary;

    public FloorFacade(Dictionary<Vector2, char> rooms, Dictionary<Vector2, Room> roomObjectDictionary)
    {
        this.rooms = rooms;
        this.roomObjectDictionary = roomObjectDictionary;
    }

    public void ConnectRoomsAround(Vector2 pickedPos, Room brRoom)
    {
        TryConnect(pickedPos, Vector2.up, brRoom);
        TryConnect(pickedPos, Vector2.down, brRoom);
        TryConnect(pickedPos, Vector2.right, brRoom);
        TryConnect(pickedPos, Vector2.left, brRoom);
    }

    private void TryConnect(Vector2 pickedPos, Vector2 direction, Room brRoom)
    {
        Vector2 targetPos = pickedPos + direction;

        if (rooms.TryGetValue(targetPos, out char checkedChar))
        {
            if (checkedChar == 'b' || checkedChar == 's')
            {
                if (roomObjectDictionary.TryGetValue(targetPos, out Room checkedRoom))
                {
                    if (direction == Vector2.up)
                    {
                        checkedRoom.connectsDown = true;
                        checkedRoom.roomConnectedDown = brRoom;

                        brRoom.connectsUp = true;
                        brRoom.roomConnectedUp = checkedRoom;
                    }
                    else if (direction == Vector2.down)
                    {
                        checkedRoom.connectsUp = true;
                        checkedRoom.roomConnectedUp = brRoom;

                        brRoom.connectsDown = true;
                        brRoom.roomConnectedDown = checkedRoom;
                    }
                    else if (direction == Vector2.right)
                    {
                        checkedRoom.connectsLeft = true;
                        checkedRoom.roomConnectedLeft = brRoom;

                        brRoom.connectsRight = true;
                        brRoom.roomConnectedRight = checkedRoom;
                    }
                    else if (direction == Vector2.left)
                    {
                        checkedRoom.connectsRight = true;
                        checkedRoom.roomConnectedRight = brRoom;

                        brRoom.connectsLeft = true;
                        brRoom.roomConnectedLeft = checkedRoom;
                    }
                }
            }
        }
    }
}
