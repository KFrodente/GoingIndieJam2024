using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashChargeWeapon : ChargeWeapon
{
	[SerializeField] protected bool affectRotation;
	
	protected void FixedUpdate()
	{
		if(charging && affectRotation)
		{
			//Debug.Log("SPEED: " + bc.GetStats().ChargeTurnSpeed);
			bc.movement.SetTargetAngle(savedTarget.GetDirection(), true);
			bc.movement.AngleTowardTargetAngle(bc.GetStats().ChargeTurnSpeed, bc);
			// Slow down current movement code
		}
	}

	public bool inDash => Time.time - lastFireTime < weaponData.attackDuration;
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Damagable d) && inDash && d.IsPlayer != savedTarget.shotByPlayer)
		{
			d.TakeDamage((int)bc.GetStats().Damage, defaultDamageType);
		}
	}
	protected void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.TryGetComponent(out Damagable d) && inDash && d.IsPlayer != savedTarget.shotByPlayer)
		{
			d.TakeDamage((int)bc.GetStats().Damage, defaultDamageType);
		}
	}

	public override bool StartAttack(Target target, BaseCharacter c)
	{
		if (!delayOver || !isCancelOver) return false;
		base.StartAttack(target, c);
		float rotationFreezeMultiplier = 2; // Find proper value
		bc.movement.Freeze(weaponData.chargeTime, weaponData.chargeTime * rotationFreezeMultiplier);
		return true;
	}

	public override void EndAttack()
	{
		if (charging && isCancelOver) CancelAttack();
	}

	protected void CancelAttack()
	{
		bc.movement.UnFreeze();
		charging = false;
		startCancelTime = Time.time;

	}

	protected override void Fire(Target target)
	{
		if(weaponData.attackSound && AudioManager.instance) AudioManager.instance.Play(weaponData.attackSound);
		bc.movement.Move(target.GetDirection(), bc.GetStats().ChargeSpeed, ForceMode2D.Impulse, bc, true);
		bc.damageable.GainImmunity(weaponData.attackDuration);
		bc.movement.SetTargetAngle(savedTarget.GetDirection(), true);
		lastFireTime = Time.time;
	}
	

	
}
