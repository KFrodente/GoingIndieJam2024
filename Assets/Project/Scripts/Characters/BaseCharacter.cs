using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public BaseInput input;
    public BaseMovement movement;
    public Weapon weapon;
    [SerializeField] protected StatHandler characterStats;
    public Rigidbody2D rb;
    public Damagable damageable;
    public EffectPlayer effector;
    [HideInInspector] public BaseCharacter possessingSpirit;

    public virtual Stats.Stats GetStats()
    {
        if (possessingSpirit != null) return characterStats.baseStats.MultiplyModifier(possessingSpirit.characterStats.modifierStats);
        return characterStats.baseStats;
    }
    protected virtual void Awake()
    {
        movement.SetCharacter(this);
    }

    protected virtual void Attack(Target target)
    {
        weapon.StartAttack(target, this);
    }

    protected virtual void Reposition(Vector2 normalizedDirection)
    {
        movement.Move(normalizedDirection, GetStats().MoveSpeed, ForceMode2D.Force, this);
    }

    protected virtual void Update()
    {
        if(input.GetMouseInput().leftDown) Attack(new Target(true, null, transform.position, true));
    }

    protected virtual void FixedUpdate()
    {
        Vector2 moveDirection = input.GetNormalizedMoveDirection();
        if(moveDirection != Vector2.zero) Reposition(moveDirection);
    }
}