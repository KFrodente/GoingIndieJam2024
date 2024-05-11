using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationWeapon : Weapon
{
	
    
    protected bool attackInAffect => Time.time - duration < weaponData.attackDuration;
    protected float duration = 0;
    public override void StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return;
        bc = c;
        Fire(target);
        duration = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Damagable d) && attackInAffect)
        {
            d.TakeDamage((int)bc.GetStats().Damage);
        }
    }
}
