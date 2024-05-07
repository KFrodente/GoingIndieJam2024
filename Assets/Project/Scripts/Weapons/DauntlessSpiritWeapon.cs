using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessSpiritWeapon : ChargeWeapon
{
	[SerializeField] private BaseCharacter character;
	[SerializeField] private DauntlessSpiritMovement movement;

	private void FixedUpdate()
	{
		if(charging)
		{
			// Rotations
			if (!movement.overrideRotation) movement.overrideRotation = true;
			SetTargetAngle(GetMousePosition() - (Vector2)transform.position);
			AngleTowardTargetAngle(10);

			// Charge back / Restraint force
			character.rb.AddForce(-transform.up * (character.statHandler.stats.MoveSpeed * 0.8f), ForceMode2D.Force);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
		{
			d.TakeDamage(character.statHandler.stats.Damage);
		}
	}

	public override void StartAttack(Vector2 target)
	{
		base.StartAttack(target);
	}

	public override void EndAttack(Vector2 target)
	{
		base.EndAttack(target);
		movement.overrideRotation = false;
	}

	public override void DoChargeAttack()
	{
		base.DoChargeAttack();

		Vector2 dir = GetMousePosition() - (Vector2)transform.position;
		// Charging force
		character.rb.AddForce(dir.normalized * 30, ForceMode2D.Impulse);

		EndAttack(dir); // needs something? so im just passing dir
	}


	// Angling for ram look
	private void SetTargetAngle(Vector3 direction)
	{
		targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
	}

	private float targetAngle;
	private void AngleTowardTargetAngle(float speed)
	{
		Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);
	}
}
