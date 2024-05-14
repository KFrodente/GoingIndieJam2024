using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaKnockbackWeapon : Weapon
{
    
    
    protected override void Fire(Target target)
    {
        Collider2D[] characterInRange = Physics2D.OverlapCircleAll(transform.position, bc.GetStats().AttackRange, LayerMask.GetMask("Character"));
        foreach (Collider2D character in characterInRange)
        {
            if(character.transform.Equals(transform)) continue;
            if (character.TryGetComponent(out Damagable d))
            {
                d.TakeDamage((int)bc.GetStats().Damage, defaultDamageType);
            }
            if (character.TryGetComponent(out BaseMovement movement))
            {
                movement.ExplodeAway(transform.position, weaponData.knockback);
            }
        }
        lastFireTime = Time.time;
    }
}
