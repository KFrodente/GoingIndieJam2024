using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floor___Stats", menuName = "Generation/Floor")]
public class FloorStatsSO : ScriptableObject
{
    [Range(0, 50)]public int basicRoomAmount;
    [Range(0, 50)]public int treasureRoomAmount;
    [Range(0, 50)]public int shopRoomAmount;

    [Range(0, 10)]public int minBossDistance;
    [Range(0, 10)]public int minTreasureDistance;
    [Range(0, 10)]public int minShopDistance;

    [Range(0, 200)]public int roomWidth;
    [Range(0, 200)]public int roomHeight;
    public Vector2 roomOffset;
    [Range(0, 1)]public float fillPercentage;
    [Range(0, 200)]public int timesToThicken;
    [Range(0, 1)]public float chanceToThicken;
    [Range(0, 1)]public float redirectChance;
    [Range(0, 1)]public float removeChance;
    [Range(0, 1)]public float createChance;
}
