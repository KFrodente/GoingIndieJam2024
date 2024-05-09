using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject
{
    [Header("Base Stats")]
    public float damage;

    public float defense;

    public float moveSpeed;
    public float chargeSpeed;
    public float maxMoveSpeed;
    public float turnSpeed;

    public float range;

    [Header("Modifier Stats")]
    public float damageMult;
    public float defenceMult;
    public float moveSpeedMult;
    public float chargeSpeedMult;
    public float maxMoveSpeedMult;
    public float turnSpeedMult;
    public float rangeMult;

    
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
        Range,
        
        Mult_Damage,
        Mult_Defence,
        Mult_MoveSpeed,
        Mult_ChargeSpeed,
        Mult_MaxMoveSpeed,
        Mult_TurnSpeed,
        Mult_Range
    }
}