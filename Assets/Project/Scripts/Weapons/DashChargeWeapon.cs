using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashChargeWeapon : ChargeWeapon
{

	
	private void FixedUpdate()
	{
		if(charging)
		{
			bc.movement.SetTargetAngle(savedTarget.GetDirection(), true);
			bc.movement.AngleTowardTargetAngle(bc.characterStats.stats.TurnSpeed, bc);
			// Slow down current movement code
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (charging && other.TryGetComponent(out Damagable d))
		{
			d.TakeDamage((int)bc.characterStats.stats.Damage);
		}
	}

	public override void StartAttack(Target target, BaseCharacter c)
	{
		base.StartAttack(target, c);
		float rotationFreezeMultiplier = 2; // Find proper value
		bc.movement.Freeze(weaponData.chargeTime, weaponData.chargeTime * rotationFreezeMultiplier);
	}

	public override void EndAttack()
	{
		if (charging) CancelAttack();
	}

	protected void CancelAttack()
	{
		bc.movement.UnFreeze();
		charging = false;
		
	}

	protected override void Fire(Vector2 normalizedDirection, bool shotByPlayer)
	{
		bc.movement.Move(transform.up, bc.characterStats.stats.ChargeSpeed, ForceMode2D.Impulse, bc, true);
		lastFireTime = Time.time;
	}


	
}
