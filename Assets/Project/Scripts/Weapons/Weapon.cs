using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponObjectData weaponData;
    protected int index = 0;
    protected bool delayOver => Time.time - lastFireTime > weaponData.fireDelay;
    protected float lastFireTime = 0;
    protected Target savedTarget;
    protected BaseCharacter bc;

    
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
        ProjectileObject po = GetProjectile();
        if(po)
        {
            Instantiate(po.projectileObject, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>().Initialize(po, shotByPlayer);
        }
        
        lastFireTime = Time.time;
    }


   
    
    protected ProjectileObject GetProjectile()
    {
        ProjectileObject po = null;
        switch (weaponData.pattern)
        {
            case ProjectilePattern.Ordered:
            {
                po = weaponData.projectileObject[index];
                index++;
                if (index == weaponData.projectileObject.Count) index = 0;
                break;
            }
            case ProjectilePattern.Random:
            {
                po = weaponData.projectileObject[Random.Range(0, weaponData.projectileObject.Count - 1)];
                break;
            }
            case ProjectilePattern.Single:
            {
                po = weaponData.projectileObject[0];
                break;
            }
            default:
            {
                break;
            }
        }

        return po;

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