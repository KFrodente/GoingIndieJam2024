using UnityEngine;

public class BurstWeapon : AutoFireWeapon
{
    protected int shotCount = 0;
    public override bool StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return false;
        bc = c;
        shotCount = weaponData.burstAmount;
        attacking = true;
        return true;
    }
    protected void Update()
    {
        if (shotCount > 0)
        {
            if (IsAutoFireDelayOver())
            {
                shotCount--;
                Fire(savedTarget);
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