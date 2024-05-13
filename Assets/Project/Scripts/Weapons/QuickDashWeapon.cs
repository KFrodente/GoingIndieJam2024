using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickDashWeapon : DurationWeapon
{
	
    

    public override bool StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return false;
        bc = c;
        duration = Time.time;
        c.movement.Freeze(weaponData.attackDuration, weaponData.attackDuration);
        Fire(target);
        c.effector?.Play(Vector2.zero, weaponData.attackDuration);
        return true;
        //c.immunity.Gain(duration = 0.15)
        
    }
    protected override void Fire(Target target)
    {
        bc.movement.Move(transform.up, bc.GetStats().ChargeSpeed, ForceMode2D.Impulse, bc, true);
        bc.damageable.GainImmunity(weaponData.attackDuration);
        lastFireTime = Time.time;
    }
    
}
