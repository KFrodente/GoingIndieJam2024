using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected int hits;
    protected ProjectileObject po;
    protected bool shotByPlayer;
    [SerializeField] protected Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        // if (other.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
        // {
        //     Debug.Log("DAMAGED");
        //     d.TakeDamage(po.damage);
        //     hits++;
        //     if(hits > po.pierceCount) Destroy(this.gameObject);
        // }
    }

    
     public void Initialize(ProjectileObject po, bool playerShot)
     {
         shotByPlayer = playerShot;
         this.po = po;
         spawnTime = Time.time;
         rb.velocity = transform.up * po.speed;
     }

    protected float spawnTime;

    

    protected void Update()
    {
        transform.position += transform.up * (Time.deltaTime * po.speed);
        if(po.lifetime > 0 && spawnTime + po.lifetime > Time.time) Destroy(this.gameObject);
    }
}
