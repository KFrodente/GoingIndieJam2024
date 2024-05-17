using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternShotWeapon : Weapon
{
	[Tooltip("if the pattern should spawn on the target")]
	public bool spawnOnTarget;
	[Tooltip("if the pattern should not parent to the target")]
	public bool dontParentToTarget;
	[Tooltip("if the pattern should not rotate towards the target")]
	public bool dontRotateTowardTarget;

	protected override void Fire(Target target)
    {
		float angle = InputUtils.GetAngle(Vector2.right);
		if (dontRotateTowardTarget) InputUtils.GetAngle(target.GetDirection());
        
		Transform parenttransform = bc.transform;
		Vector3 spawnposition = parenttransform.position;

		if (spawnOnTarget)
		{
			if (target.type == TargetType.Character)
			{
				if (dontParentToTarget)
				{
					parenttransform = null;
				} else
				{
					parenttransform = target.characterTarget.parent;
				}
			}
			if (target.type == TargetType.Mouse || target.type == TargetType.Position)
			{
				parenttransform = null;
			}

			spawnposition = target.GetTargetPosition();//
		}

		if (dontParentToTarget)
		{
			parenttransform = null;
		}

		Instantiate(weaponData.projectile, spawnposition, Quaternion.Euler(0, 0, angle), parenttransform).GetComponent<Projectile>().Initialize(target, (int)bc.GetStats().Damage);
        //e.Initialize(target, (int)bc.GetStats().Damage);
        //e.transform.SetParent(transform);
        lastFireTime = Time.time;
    }

}
