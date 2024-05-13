using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternShotWeapon : AutoFireWeapon
{
    protected virtual void Fire(Target target)
    {
        float angle = InputUtils.GetAngle(target.GetDirection());
        Projectile e = Instantiate(weaponData.projectile, transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Projectile>();
        e.Initialize(target, (int)bc.GetStats().Damage);
        e.transform.SetParent(transform);
        lastFireTime = Time.time;
    }

}
