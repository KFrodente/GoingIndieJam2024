using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvidSpiritWeapon : Weapon
{
	[SerializeField] private BaseCharacter character;

    private bool doDamage = false;
    private float damageTimer;

    [SerializeField] private float attackCooldownTimer;
    [SerializeField] private float attackCooldownTime = 4f;

    private void Update()
    {
        //base.Update();
        if (doDamage)
        {
            if (damageTimer > 0) damageTimer -= Time.deltaTime;
            else doDamage = false;
        }
        if (attackCooldownTimer > 0) attackCooldownTimer -= Time.deltaTime;
    }


    public override void StartAttack(Target target, BaseCharacter c)
    {
        if (attackCooldownTimer <= 0)
        {
            base.StartAttack(target, c);
            //character.rb.AddForce(transform.up * 20, ForceMode2D.Impulse);
            damageTimer = 0.15f;
            doDamage = true;
            attackCooldownTimer = attackCooldownTime;
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
