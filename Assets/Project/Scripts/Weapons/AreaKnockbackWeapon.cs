using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaKnockbackWeapon : Weapon
{
    
    
    protected override void Fire(Vector2 normalizedDirection, bool shotByPlayer)
    {
        Debug.Log("FIRED");
        Collider2D[] characterInRange = Physics2D.OverlapCircleAll(transform.position, bc.GetStats().AttackRange, LayerMask.GetMask("Character"));
        foreach (Collider2D character in characterInRange)
        {
            Debug.Log("Character: " + character.name);
            if(character.transform.Equals(transform)) continue;
            if (character.TryGetComponent(out Damagable d))
            {
                d.TakeDamage((int)bc.GetStats().Damage);
            }
            if (character.TryGetComponent(out BaseMovement movement))
            {
                movement.ExplodeAway(transform.position, weaponData.knockback);
            }
        }
        lastFireTime = Time.time;
    }
}
