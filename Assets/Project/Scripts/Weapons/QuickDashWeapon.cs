using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickDashWeapon : DurationWeapon
{
	
    

    public override void StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return;
        bc = c;
        duration = Time.time;
        c.movement.Freeze(weaponData.attackDuration, weaponData.attackDuration);
        Fire(target.GetDirection(), target.playerTargeting);
        //c.effect?.play(data);
        //c.immunity.Gain(duration = 0.15)
        
    }
    protected override void Fire(Vector2 normalizedDirection, bool shotByPlayer)
    {
        bc.movement.Move(transform.up, bc.characterStats.stats.ChargeSpeed, ForceMode2D.Impulse, bc, true);
        lastFireTime = Time.time;
    }
    
}
