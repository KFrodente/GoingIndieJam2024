using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvidSpiritWeapon : Weapon
{
	[SerializeField] private BaseCharacter character;
    [SerializeField] private Damagable damagable;

    private bool doDamage = false;
    private float damageTimer;

    [SerializeField] private float attackCooldownTimer;
    [SerializeField] private float attackCooldownTime = 4f;

    private Vector2 lastImagePos;
    private void Update()
    {
        //base.Update();
        if (doDamage)
        {
            if (damageTimer > 0) damageTimer -= Time.deltaTime;
            else doDamage = false;
            if (((Vector2)transform.position - lastImagePos).magnitude > 2)
            {
                AfterImagePool.Instance.GetFromPool();
                lastImagePos = transform.position;
            }
        }
        if (attackCooldownTimer > 0) attackCooldownTimer -= Time.deltaTime;
    }


    public override void StartAttack(Vector2 target, BaseCharacter c)
    {
        if (attackCooldownTimer <= 0)
        {
            base.StartAttack(target);
            character.rb.AddForce(transform.up * character.statHandler.stats.Range, ForceMode2D.Impulse);
            damageTimer = 0.15f;
            doDamage = true;
            damagable.StartImmunity(damageTimer);
            attackCooldownTimer = attackCooldownTime;
            AfterImagePool.Instance.GetFromPool();
            lastImagePos = transform.position;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if (doDamage && other.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
        // {
        //     // d.TakeDamage(character.statHandler.stats.Damage);
        //     // attackCooldownTimer = 0;
        // }
    }
}
