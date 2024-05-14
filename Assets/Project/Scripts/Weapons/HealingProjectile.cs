using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingProjectile : Projectile
{
    [SerializeField] protected int healthIncrease;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!initialized) return;
        if (other.TryGetComponent(out Damagable d) && d.IsPlayer != target.shotByPlayer)
        {
            d.TakeDamage(damage, projectileData.type);
            if(onHitEffect != null) d.baseCharacter.characterStats.AddStatModifier(onHitEffect.GetModifier());
            d.Health += healthIncrease;
            hits++;
            if(projectileData.hitSound != null) AudioManager.instance.Play(projectileData.hitSound);
            if(projectileData.hitParticle != null) Instantiate(projectileData.hitParticle, transform.position, transform.rotation);
            if(hits > projectileData.pierceCount) Destroy(this.gameObject);
        }
    }
    
}
