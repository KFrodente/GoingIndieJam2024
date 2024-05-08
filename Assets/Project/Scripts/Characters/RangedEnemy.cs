using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : BaseCharacter
{
    [SerializeField] protected float attackRange;
    public override void Attack(Vector2 target)
    {
        if(Vector2.Distance(target, transform.position) < attackRange) weapon.StartAttack(target);
    }

    public override void Reposition(Vector2 target)
    {
        float distance = Vector2.Distance(target, transform.position);
        if(distance > attackRange) movement.Move((target - (Vector2)transform.position));
        else if(distance < attackRange * 0.5f) movement.Move((Vector2)transform.position - target);
    }
    
    
}
