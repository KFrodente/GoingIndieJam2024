using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floor___Stats", menuName = "Generation/Floor")]
public class FloorStatsSO : ScriptableObject
{
    public int roomWidth;
    public int roomHeight;
    public Vector2 roomOffset;
    public float fillPercentage;
    public int timesToThicken;
    public float chanceToThicken;
    public float redirectChance;
    public float removeChance;
    public float createChance;
}
