using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected ProjectileObject projectileData;
    protected int hits;
    protected bool shotByPlayer;
    [SerializeField] protected Rigidbody2D rb;
    protected float dMult = 1;
    protected float spawnTime;
     public virtual void Initialize(bool playerShot, float damageMultiplier)
     {
         dMult = damageMultiplier;
         shotByPlayer = playerShot;
         spawnTime = Time.time;
         rb.velocity = transform.up * projectileData.speed;
     }
     
     
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Damagable d) && d.IsPlayer != shotByPlayer)
        {
            d.TakeDamage((int)(projectileData.damage * dMult));
            hits++;
            if(projectileData.hitSound != null) AudioManager.instance.Play(projectileData.hitSound);
            if(projectileData.hitParticle != null) Instantiate(projectileData.hitParticle, transform.position, Quaternion.identity);
            if(hits > projectileData.pierceCount) Destroy(this.gameObject);
        }
    }



    

    protected virtual void Update()
    {
        if(projectileData.lifetime > 0 && Time.time > spawnTime + projectileData.lifetime ) DestroyProjectile();
    }

    protected virtual void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
