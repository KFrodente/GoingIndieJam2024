using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject
{
    [Header("Base Stats")]
    public float damage;
    public float moveSpeed;
    public float chargeSpeed;
    public float turnSpeed;
    public float attackRange;
    public float attackSpeed;
    public float chargeTurnSpeed;

    [Header("Modifier Stats")]
    public float damageMult;
    public float moveSpeedMult;
    public float chargeSpeedMult;
    public float turnSpeedMult;
    public float attackRangeMult;
    public float attackSpeedMult;
    public float chargeTurnSpeedMult;

}

namespace Stats
{
    
    public enum StatType
    {
        None,
        Damage,
        MoveSpeed,
        ChargeSpeed,
        TurnSpeed,
        AttackRange,
        AttackSpeed,
        ChargeTurnSpeed,
        
        
        Mult_Damage,
        Mult_MoveSpeed,
        Mult_ChargeSpeed,
        Mult_TurnSpeed,
        Mult_AttackRange,
        Mult_AttackSpeed,
        Mult_ChargeTurnSpeed
    }
}