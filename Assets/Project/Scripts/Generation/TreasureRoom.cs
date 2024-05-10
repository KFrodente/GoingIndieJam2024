using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoom : Room
{
    public Transform upTelePos;
    public Transform rightTelePos;
    public Transform downTelePos;
    public Transform leftTelePos;

    [Tooltip("list of locations where Spirit Essence can spawn")]
    public List<Transform> SELocations = new();

    public override void GeneratePortals()
    {
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

        GenerateSpiritEssence();
    }

    private void GenerateSpiritEssence()
    {
       
    }
}
