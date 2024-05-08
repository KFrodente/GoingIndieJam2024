using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Floor___TreasureRoom", menuName = "Generation/Treasure Room")]
public class TreasureRoomSO : ScriptableObject
{
    [Range(0, 50)] public int treasureRoomAmount;
    [Range(0, 10)] public int minTreasureDistance;
}
