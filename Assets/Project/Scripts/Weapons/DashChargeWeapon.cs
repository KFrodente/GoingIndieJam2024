using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashChargeWeapon : ChargeWeapon
{

	
	protected void FixedUpdate()
	{
		if(charging)
		{
			bc.movement.SetTargetAngle(savedTarget.GetDirection(), true);
			bc.movement.AngleTowardTargetAngle(bc.characterStats.stats.TurnSpeed, bc);
			// Slow down current movement code
		}
	}

	protected bool inDash => Time.time - lastFireTime < weaponData.attackDuration;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Damagable d) && inDash)
		{
			d.TakeDamage((int)bc.characterStats.stats.Damage);
		}
	}

	public override void StartAttack(Target target, BaseCharacter c)
	{
		if (!delayOver) return;
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
		lastFireTime = Time.time + weaponData.cancelDelay;
		
	}

	protected override void Fire(Vector2 normalizedDirection, bool shotByPlayer)
	{
		bc.movement.Move(transform.up, bc.characterStats.stats.ChargeSpeed, ForceMode2D.Impulse, bc, true);
		lastFireTime = Time.time;
	}
	

	
}
