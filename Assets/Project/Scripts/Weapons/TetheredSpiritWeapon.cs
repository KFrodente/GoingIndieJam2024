using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredSpiritWeapon : Weapon
{
	[SerializeField] private BaseCharacter character;

    [SerializeField] private float attackCooldownTimer;
    [SerializeField] private float attackCooldownTime = 4f;

    [SerializeField] private float knockback = 5f;
    public LayerMask enemyLayer;

    private void Update()
    {
        if (attackCooldownTimer > 0) attackCooldownTimer -= Time.deltaTime;
    }

    public override void StartAttack(Target target, BaseCharacter c)
	{
        if (attackCooldownTimer <= 0)
        {
            Debug.Log("starting attack");
            base.StartAttack(target);
            attackCooldownTimer = attackCooldownTime;
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, character.statHandler.stats.Range, enemyLayer);
            foreach (Collider2D enemy in enemiesInRange)
            {
                Debug.Log("enemy detected");
                // Damage
                if (enemy.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
                {
                    d.TakeDamage(character.statHandler.stats.Damage);
                }

                // Knockback
                Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
                if (enemyRigidbody != null)
                {
                    Vector2 direction = (enemy.transform.position - transform.position).normalized;
                    enemyRigidbody.AddForce(direction * knockback, ForceMode2D.Impulse);
                }
            }
        }
    }
}
