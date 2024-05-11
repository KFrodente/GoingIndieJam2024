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
			bc.movement.SetTargetAngle(savedTarget.GetDirection(), true);
			bc.movement.AngleTowardTargetAngle(bc.GetStats().ChargeTurnSpeed, bc);
			// Slow down current movement code
		}
	}

	protected bool inDash => Time.time - lastFireTime < weaponData.attackDuration;
	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Damagable d) && inDash)
		{
			Debug.Log("HURT: " + d.gameObject.name);
			d.TakeDamage((int)bc.GetStats().Damage);
		}
	}
	protected void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.TryGetComponent(out Damagable d) && inDash)
		{
			d.TakeDamage((int)bc.GetStats().Damage);
		}
	}

	public override void StartAttack(Target target, BaseCharacter c)
	{
		Debug.Log("ATTACK " + !delayOver + " " + !isCancelOver + " AttackSPeed: " + bc.GetStats().AttackSpeed);
		if (!delayOver || !isCancelOver) return;
		base.StartAttack(target, c);
		float rotationFreezeMultiplier = 2; // Find proper value
		bc.movement.Freeze(weaponData.chargeTime, weaponData.chargeTime * rotationFreezeMultiplier);
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

	protected override void Fire(Vector2 normalizedDirection, bool shotByPlayer)
	{
		bc.movement.Move(normalizedDirection, bc.GetStats().ChargeSpeed, ForceMode2D.Impulse, bc, true);
		bc.damageable.StartImmunity(weaponData.attackDuration);
		lastFireTime = Time.time;
	}
	

	
}
