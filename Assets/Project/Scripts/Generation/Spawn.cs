using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : Room
{
    public Transform spawnPos;

    public Transform upTelePos;
    public Transform rightTelePos;
    public Transform downTelePos;
    public Transform leftTelePos;

    public override void GeneratePortals()
    {
        base.GeneratePortals();

        if (connectsUp)
        {
            topPortal = GenerateRespectivePortal(roomConnectedUp, upTelePos).GetComponent<Portal>();
        }
        if (connectsRight)
        {
            rightPortal = GenerateRespectivePortal(roomConnectedRight, rightTelePos).GetComponent<Portal>();
        }
        if (connectsDown)
        {
            bottomPortal = GenerateRespectivePortal(roomConnectedDown, downTelePos).GetComponent<Portal>();
        }
        if (connectsLeft)
        {
            leftPortal = GenerateRespectivePortal(roomConnectedLeft, leftTelePos).GetComponent<Portal>();
        }
    }


}
