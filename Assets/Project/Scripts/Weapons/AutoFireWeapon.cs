using UnityEngine;

public class AutoFireWeapon : Weapon
{
    protected bool attacking;
    protected float lastAutoFireTime = 0;
    
    public override void StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return;
        attacking = true;
    }
    public override void EndAttack()
    {
        attacking = false;
    }
    protected void Update()
    {
        if (attacking && IsAutoFireDelayOver())
        {
            Fire(savedTarget.GetDirection(), savedTarget.playerTargeting);
        }
    }

    protected virtual bool IsAutoFireDelayOver()
    {
        return Time.time - lastAutoFireTime > weaponData.fireDelay;
        
    }
}