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
        FloorGenerator.instance.corvid.transform.position = spawnPos.position;
        FloorGenerator.instance.dauntless.transform.position = spawnPos.position;
        FloorGenerator.instance.tethered.transform.position = spawnPos.position;

        base.GeneratePortals();

        if (connectsUp)
        {
            topPortal = GenerateRespectivePortal(roomConnectedUp, upTelePos.position).GetComponent<Portal>();
        }
        if (connectsRight)
        {
            rightPortal = GenerateRespectivePortal(roomConnectedRight, rightTelePos.position).GetComponent<Portal>();
        }
        if (connectsDown)
        {
            bottomPortal = GenerateRespectivePortal(roomConnectedDown, downTelePos.position).GetComponent<Portal>();
        }
        if (connectsLeft)
        {
            leftPortal = GenerateRespectivePortal(roomConnectedLeft, leftTelePos.position).GetComponent<Portal>();
        }
    }
}
