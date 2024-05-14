using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponObjectData weaponData;
    [SerializeField] protected ProjectileDamageType defaultDamageType;
    protected int index = 0;
    public bool delayOver => Time.time - lastFireTime > bc.GetStats().AttackSpeed;
    protected float lastFireTime = 0;
    protected Target savedTarget;
    protected BaseCharacter bc;

    public virtual void InitializeCharacter(BaseCharacter c)
    {
        bc = c;
    }

    
    public virtual bool StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return false;
        bc = c;
        savedTarget = target;
        Fire(target);
        return true;
    }

    public virtual void EndAttack()
    {
    }

    protected virtual void Fire(Target target)
    {
        float angle = InputUtils.GetAngle(target.GetDirection());
        Instantiate(weaponData.projectile, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>().Initialize(target, (int)bc.GetStats().Damage);
        
        lastFireTime = Time.time;
    }

    

   
    
    protected virtual void Start()
    {
        lastFireTime = -bc.GetStats().AttackSpeed;
    }
}