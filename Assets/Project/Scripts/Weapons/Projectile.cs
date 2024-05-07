using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int hits;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TryGetComponent(out Damagable d)) //is player
        {
            d.TakeDamage(po.damage);
            hits++;
            if(hits > po.pierceCount) Destroy(this.gameObject);
        }
    }
    private ProjectileObject po;

    public void SetProjectile(ProjectileObject po)
    {
        this.po = po;
    }
    

    protected void Update()
    {
        transform.position += transform.up * (Time.deltaTime * po.speed);
    }
}
