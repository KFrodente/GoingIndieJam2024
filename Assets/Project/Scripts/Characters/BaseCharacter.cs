using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField] protected Weapon weapon;
    [SerializeField] protected CharacterMovement movement;
    public StatHandler statHandler;
    [SerializeField] public float attackRange;
    [SerializeField] public Rigidbody2D rb;

    
    public virtual void Attack(Vector2 target)
    {
        
    }

    public virtual void Reposition(Vector2 target)
    {
        
    }
}