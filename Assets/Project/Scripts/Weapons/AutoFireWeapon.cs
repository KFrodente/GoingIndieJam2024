using UnityEngine;

public class AutoFireWeapon : Weapon
{
    protected bool attacking;
    protected float lastAutoFireTime = 0;
    
    public override void StartAttack(Target target, BaseCharacter c)
    {
        savedTarget = target;
        if (!delayOver) return;
        attacking = true;
        bc = c;
    }
    public override void EndAttack()
    {
        attacking = false;
    }
    protected void Update()
    {
        if (attacking && IsAutoFireDelayOver())
        {
            Fire(savedTarget);
            lastAutoFireTime = Time.time;
        }
    }

    protected virtual bool IsAutoFireDelayOver()
    {
        return Time.time - lastAutoFireTime > bc.GetStats().AttackSpeed;
        
    }
}