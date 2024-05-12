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
    protected float spawnTime;
    protected int damage = 0;
     
     public void Initialize(bool playerShot, int damage)
     {
         shotByPlayer = playerShot;
         spawnTime = Time.time;
         rb.velocity = transform.up * projectileData.speed;
         this.damage = damage;
     }
     
     
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Damagable d) && d.IsPlayer != shotByPlayer)
        {
            d.TakeDamage(damage);
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
