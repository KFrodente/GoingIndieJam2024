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
    public Stats.OperatorType operatorType1;
    public float Value1;

    [Header("Benefit 2")]
    public Stats.StatType statType2;
    public Stats.OperatorType operatorType2;
    public float Value2;

    [Header("Benefit 3")]
    public Stats.StatType statType3;
    public Stats.OperatorType operatorType3;
    public float Value3;

    public enum EssenceTier
    {
        SS,
        S,
        A,
        B,
        C
    }

}
