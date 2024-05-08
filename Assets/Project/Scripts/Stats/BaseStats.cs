using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/BaseStats")]
public class BaseStats : ScriptableObject
{
    public int maxHealth;
    public int health;
    
    public int maxStamina;
    public int stamina;

    public int damage;

    public int defense;

    public int moveSpeed;
    public int maxMoveSpeed;
    public int turnSpeed;

    public int range;

}


namespace Stats
{
    
    public enum StatType
    {
        Health,
        Damage,
        Defence,
        MoveSpeed,
        MaxMoveSpeed,
        TurnSpeed,
        Range
    }
}