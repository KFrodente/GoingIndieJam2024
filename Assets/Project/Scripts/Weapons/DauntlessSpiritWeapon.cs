using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessSpiritWeapon : ChargeWeapon
{
	[SerializeField] private BaseCharacter character;
	[SerializeField] private DauntlessSpiritMovement movement;
	[SerializeField] private Damagable damagable;

	private bool doDamage = false;
	private float damageTimer;

	[SerializeField] private float attackCooldownTimer;
	[SerializeField] private float attackCooldownTime = 4f;
    [SerializeField] private float knockback = 10f;

    private void Update()
    {
        base.Update();
        if (doDamage)
        {
            if (damageTimer > 0) damageTimer -= Time.deltaTime;
            else doDamage = false;
        }
        if (attackCooldownTimer > 0) attackCooldownTimer -= Time.deltaTime;
    }

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
		if (doDamage && other.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
		{
			d.TakeDamage(character.statHandler.stats.Damage);
			attackCooldownTimer = 0;

            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                enemyRigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
            }
        }
	}

	public override void StartAttack(Vector2 target)
	{
		if (attackCooldownTimer <= 0)
		{
			base.StartAttack(target);
			attackCooldownTimer = attackCooldownTime;
		}
	}

	public override void EndAttack(Vector2 target)
	{
		base.EndAttack(target);
		movement.overrideRotation = false;
		//hitCount = 0;
	}

	public override void DoChargeAttack()
	{
		base.DoChargeAttack();

		Vector2 dir = GetMousePosition() - (Vector2)transform.position;
		// Charging force
		character.rb.AddForce(dir.normalized * character.statHandler.stats.Range, ForceMode2D.Impulse);

		damageTimer = 0.3f;
		doDamage = true;

		damagable.StartImmunity(damageTimer);

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
