using UnityEngine;

public class AutoFireWeapon : Weapon
{
    public bool attacking;
    protected float lastAutoFireTime = 0;
    
    public override bool StartAttack(Target target, BaseCharacter c)
    {
        savedTarget = target;
        if (!delayOver) return false;
        attacking = true;
        bc = c;
        return true;

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

    public virtual bool IsAutoFireDelayOver()
    {
        return Time.time - lastAutoFireTime > bc.GetStats().AttackSpeed;
        
    }
}