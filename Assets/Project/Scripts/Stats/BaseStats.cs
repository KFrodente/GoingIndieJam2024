using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject
{

    public float damage;

    public float defense;

    public float moveSpeed;
    public float chargeSpeed;
    public float maxMoveSpeed;
    public float turnSpeed;

    public float range;

}


namespace Stats
{
    
    public enum StatType
    {
        Damage,
        Defence,
        MoveSpeed,
        ChargeSpeed,
        MaxMoveSpeed,
        TurnSpeed,
        Range
    }
}