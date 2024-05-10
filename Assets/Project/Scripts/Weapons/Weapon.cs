using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponObjectData weaponData;
    protected int index = 0;
    protected bool delayOver => Time.time - lastFireTime > weaponData.fireDelay * bc.GetStats().AttackSpeed;
    protected float lastFireTime = 0;
    protected Target savedTarget;
    protected BaseCharacter bc;

    public virtual void InitializeCharacter(BaseCharacter c)
    {
        bc = c;
    }

    
    public virtual void StartAttack(Target target, BaseCharacter c)
    {
        if (!delayOver) return;
        bc = c;
        savedTarget = target;
        Fire(target.GetDirection(), (target.playerTargeting));
    }

    public virtual void EndAttack()
    {
    }

    protected virtual void Fire(Vector2 normalizedDirection, bool shotByPlayer)
    {
        float angle = InputUtils.GetAngle(normalizedDirection);
        Instantiate(weaponData.projectile, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>().Initialize(shotByPlayer, GetProjectileDamageMult());
        
        lastFireTime = Time.time;
    }

    protected virtual float GetProjectileDamageMult()
    {
        return 1;
    }


   
    
    protected virtual void Start()
    {
        lastFireTime = -weaponData.fireDelay;
    }
}

public class Target
{
    public bool isMouse;
    public Vector2? target;
    public Vector2 fireLocation;
    public bool playerTargeting;

    public Target(bool isMouse, Vector2? target, Vector2 fireSpot, bool playerTargeting)
    {
        this.isMouse = isMouse;
        this.target = target;
        this.fireLocation = fireSpot;
        this.playerTargeting = playerTargeting;
    }

    public Vector2 GetDirection()
    {
        if (isMouse) return (InputUtils.GetMousePosition() - fireLocation).normalized;
        return (target.Value - fireLocation).normalized;
    }
    
}