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
     public void Initialize(bool playerShot, float damageMultiplier)
     {
         dMult = damageMultiplier;
         shotByPlayer = playerShot;
         spawnTime = Time.time;
         rb.velocity = transform.up * projectileData.speed;
     }
     
     
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Damagable d) && TryGetComponent(out BaseCharacter bc) && bc.input is PlayerInput != shotByPlayer) // Need more efficient way to calculate controlling entity
        {
            d.TakeDamage((int)(projectileData.damage * dMult));
            hits++;
            if(hits > projectileData.pierceCount) Destroy(this.gameObject);
        }
    }



    

    protected void Update()
    {
        if(projectileData.lifetime > 0 && spawnTime + projectileData.lifetime > Time.time) DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
