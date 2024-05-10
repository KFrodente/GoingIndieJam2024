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

    }

    public void GenerateSpiritEssence(int roomCost)
    {
        for (int i = 0; i < SELocations.Count; i++)
        {
            GetSpiritEssence(roomCost);
        }
    }

    private GameObject GetSpiritEssence(int roomCost)
    {
        float addedChance = roomCost / (float)10;
        float chance = Random.Range(0.0f, 100.0f);

        if (chance < 1 + addedChance) return SpiritEssenceHolder.instance.GetSSRankEssence();
        else if (chance < 5 + addedChance) return SpiritEssenceHolder.instance.GetSRankEssence();
        else if (chance < 15 + addedChance) return SpiritEssenceHolder.instance.GetARankEssence();
        else if (chance < 30 + addedChance) return SpiritEssenceHolder.instance.GetBRankEssence();
        else if (chance < 49 + addedChance) return SpiritEssenceHolder.instance.GetCRankEssence();
        return null;
    }
}
