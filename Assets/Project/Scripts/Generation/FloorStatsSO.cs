using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floor___Stats", menuName = "Generation/Floor")]
public class FloorStatsSO : ScriptableObject
{
    int roomWidth;
    int roomHeight;
    Vector2 roomOffset;
    float fillPercentage;
    int timesToThicken;
    float chanceToThicken;

    float redirectChance;
    float removeChance;
    float createChance;
}
