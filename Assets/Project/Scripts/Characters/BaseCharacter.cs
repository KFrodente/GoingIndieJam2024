using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public BaseInput input;
    public BaseMovement movement;
    public Weapon weapon;
    public StatHandler characterStats;
    public Rigidbody2D rb;
    [HideInInspector] public BaseCharacter possessingSpirit;

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
        movement.Move(normalizedDirection, characterStats.stats.MoveSpeed, ForceMode2D.Force, this);
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