using UnityEngine;

public class BurstWeapon : AutoFireWeapon
{
    protected int shotCount = 0;
    public override void StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return;
        bc = c;
        shotCount = weaponData.burstAmount;
        attacking = true;
    }
    protected void Update()
    {
        if (shotCount > 0)
        {
            if (IsAutoFireDelayOver())
            {
                shotCount--;
                Fire(savedTarget.GetDirection(), savedTarget.playerTargeting);
            }
        }
        else
        {
            if(attacking) StartAttack(savedTarget, bc);
        }
    }
    protected override bool IsAutoFireDelayOver()
    {
        return Time.time - lastAutoFireTime > weaponData.burstSeparationDelay;
        
    }
    
}