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
    [SerializeField] protected float timeBeforeActiveDamage;
    protected float spawnTime;
    protected int damage = 0;
    protected Target target;
    protected bool initialized;
    [SerializeField] protected StatEffect onHitEffect;
    protected bool isActive => Time.time - spawnTime > timeBeforeActiveDamage;

    [Header("Wall Info")]
    [SerializeField] protected bool wallCollision = true;
    [SerializeField] protected bool destroyOnWall = false;

    //[SerializeField] private InvisibleDestroy invisDestroy;
     
     public virtual void Initialize(Target target, int damage)
     {
         spawnTime = Time.time;
         if(rb) rb.velocity = transform.up * projectileData.speed;
         if(!projectileData.zeroDamage) this.damage = damage;
         this.target = target;
         initialized = true;
         //if(invisDestroy)invisDestroy.enabled = true;
     }
     
      
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!initialized || !isActive) return;
        if (other.TryGetComponent(out Damagable d) && d.IsPlayer != target.shotByPlayer)
        {
            bool damageTaken = d.TakeDamage(damage, projectileData.type);

            if (damageTaken && projectileData.hitSound != null) AudioManager.instance.Play(projectileData.hitSound);
            
            if(onHitEffect != null) d.baseCharacter.characterStats.AddStatModifier(onHitEffect.GetModifier());
            hits++;
            
            if(projectileData.hitParticle != null) Instantiate(projectileData.hitParticle, transform.position, transform.rotation);
            if(hits > projectileData.pierceCount) Destroy(this.gameObject);
        }

        if (wallCollision && other.gameObject.layer == LayerMask.NameToLayer("Tilemap"))
        {
            if(destroyOnWall) DestroyProjectile();
            rb.velocity = Vector2.zero;
        }
    }
    
    protected virtual void Update()
    {
        if (!initialized) return;
        if(projectileData.lifetime > 0 && Time.time > spawnTime + projectileData.lifetime ) DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        if (projectileData.despawnParticle) Instantiate(projectileData.despawnParticle, transform.position, transform.rotation);
        if (projectileData.despawnSound) AudioManager.instance.Play(projectileData.despawnSound);
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