using System;
using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileObject projectileData;
    protected int hits;
    [SerializeField] protected Rigidbody2D rb;
    protected float spawnTime;
    protected int damage = 0;
    protected Target target;
    protected bool initialized;
    [SerializeField] protected StatEffect onHitEffect;
     
     public virtual void Initialize(Target target, int damage)
     {
         spawnTime = Time.time;
         if(rb) rb.velocity = transform.up * projectileData.speed;
         this.damage = damage;
         this.target = target;
         initialized = true;
     }
     
     
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //if (!initialized) return;
        if (other.TryGetComponent(out Damagable d) && d.IsPlayer != target.shotByPlayer)
        {
            d.TakeDamage(damage, projectileData.type);
            if(onHitEffect != null) d.baseCharacter.characterStats.AddStatModifier(onHitEffect.GetModifier());
            hits++;
            if(projectileData.hitSound != null) AudioManager.instance.Play(projectileData.hitSound);
            if(projectileData.hitParticle != null) Instantiate(projectileData.hitParticle, transform.position, Quaternion.identity);
            if(hits > projectileData.pierceCount) Destroy(this.gameObject);
        }
    }
    
    protected virtual void Update()
    {
        if (!initialized) return;
        if(projectileData.lifetime > 0 && Time.time > spawnTime + projectileData.lifetime ) DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}

[Flags]
public enum ProjectileDamageType
{
    Blunt = 1,
    Acid = 2,
    Sharp = 4,
    Explosion = 8,
    Cold = 16,
    Arcane = 32,
    Fire = 64
}