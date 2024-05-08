using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessShieldWeapon : ChargeWeapon
{
	[SerializeField] private StatHandler statHandler;
	[SerializeField] private CharacterMovement movement;
	

	private bool doDamage = false;
	private float damageTimer;

	private float hitCount = 0;
	private float attackCooldownTimer;

	private void Update()
	{
		base.Update();
		if(doDamage)
		{
			if(damageTimer > 0)
			{
				damageTimer -= Time.deltaTime;
			}
			else
			{
				doDamage = false;
			}
		}
		if(attackCooldownTimer > 0)
		{
			attackCooldownTimer -= Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if(charging)
		{
			// Rotations
			movement.SetTargetAngle(GetMousePosition() - (Vector2)transform.position);

			// Charge back / Restraint force
			movement.Move(transform.up * (statHandler.stats.MoveSpeed * 0.2f), ForceMode2D.Force);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (doDamage && other.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
		{
			d.TakeDamage(statHandler.stats.Damage);
			attackCooldownTimer = 0;
		}
	}

	public override void StartAttack(Vector2 target)
	{
		if (attackCooldownTimer <= 0)
		{
			base.StartAttack(target);
			attackCooldownTimer = weapon.fireDelay;
			movement.SetMovement(false);
		}
	}

	public override void EndAttack(Vector2 target)
	{
		base.EndAttack(target);
		movement.SetMovement(true);
	}

	public override void DoChargeAttack()
	{
		base.DoChargeAttack();

		Vector2 dir = GetMousePosition() - (Vector2)transform.position;
		// Charging force
		movement.Move(dir.normalized * chargeSpeedMult, ForceMode2D.Impulse, true);
		damageTimer = 0.3f;
		doDamage = true;

		EndAttack(dir); // needs something? so im just passing dir
	}


	
}
