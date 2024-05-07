using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponObject weapon;
    protected int index = 0;
    protected bool delayOver => Time.time - lastFireTime > weapon.fireDelay;

    protected float lastFireTime = 0;
    protected float startAttackTime;
    public virtual void StartAttack(Vector2 target)
    {
        startAttackTime = Time.time;
    }

    public virtual void EndAttack(Vector2 target)
    {
    }

    protected void Fire(Vector2 target)
    {
        ProjectileObject po = GetProjectile();
        if(po)
        {
            Instantiate(po.projectileObject, transform.position, Quaternion.Euler(0, 0, GetAngle(target))).GetComponent<Projectile>().SetProjectile(po);
        }

        lastFireTime = Time.time;
    }

    protected float GetAngle(Vector2 target)
    {
        Vector2 direction = (target - (Vector2)transform.position).normalized;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }
    


    protected Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    protected ProjectileObject GetProjectile()
    {
        ProjectileObject po = null;
        switch (weapon.pattern)
        {
            case ProjectilePattern.Ordered:
            {
                po = weapon.projectileObject[index];
                index++;
                if (index == weapon.projectileObject.Count) index = 0;
                break;
            }
            case ProjectilePattern.Random:
            {
                po = weapon.projectileObject[Random.Range(0, weapon.projectileObject.Count - 1)];
                break;
            }
            case ProjectilePattern.Single:
            {
                po = weapon.projectileObject[0];
                break;
            }
            default:
            {
                break;
            }
        }

        return po;

    }
}