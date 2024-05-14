using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternShotWeapon : Weapon
{
    protected virtual void Fire(Target target)
    {
        float angle = InputUtils.GetAngle(target.GetDirection());
        Instantiate(weaponData.projectile, transform.position, Quaternion.Euler(0, 0, angle), transform).GetComponent<Projectile>().Initialize(target, (int)bc.GetStats().Damage);
        //e.Initialize(target, (int)bc.GetStats().Damage);
        //e.transform.SetParent(transform);
        lastFireTime = Time.time;
    }

}
