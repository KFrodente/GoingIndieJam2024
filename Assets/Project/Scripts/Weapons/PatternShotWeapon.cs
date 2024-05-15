using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternShotWeapon : Weapon
{
	[Tooltip("if the pattern should spawn on the target")]
	public bool spawnOnTarget;
	[ShowIf("spawnOnTarget")]public bool parentToTarget;

	protected override void Fire(Target target)
    {
        float angle = InputUtils.GetAngle(target.GetDirection());
        Transform parenttransform = bc.transform;
		Vector3 spawnposition = parenttransform.position;

		if (spawnOnTarget)
		{
			if (target.type == TargetType.Character)
			{
				if (parentToTarget)
				{
					parenttransform = target.characterTarget.parent;
				} else
				{
					parenttransform = null;
				}
			}
			if (target.type == TargetType.Mouse || target.type == TargetType.Position)
			{
				parenttransform = null;
			}

			spawnposition = target.GetTargetPosition();
		}

        Instantiate(weaponData.projectile, spawnposition, Quaternion.Euler(0, 0, angle), parenttransform).GetComponent<Projectile>().Initialize(target, (int)bc.GetStats().Damage);
        //e.Initialize(target, (int)bc.GetStats().Damage);
        //e.transform.SetParent(transform);
        lastFireTime = Time.time;
    }

}
