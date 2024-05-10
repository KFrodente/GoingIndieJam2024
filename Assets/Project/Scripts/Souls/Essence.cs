using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Essence", menuName = "Essence")]
public class Essence : ScriptableObject
{
    [Header("Essence Properties")]
    public string essenceName;
    public int soulCost;
    public EssenceTier tier;

    [Header("Benefit 1")]
    public Stats.StatType statType1;
    public float AddedChange1;
    public float MultipliedChange1;

    [Header("Benefit 2")]
    public Stats.StatType statType2;
    public float AddedChange2;
    public float MultipliedChange2;

    [Header("Benefit 3")]
    public Stats.StatType statType3;
    public float AddedChange3;
    public float MultipliedChange3;

    public enum EssenceTier
    {
        SS,
        S,
        A,
        B,
        C
    }

}
