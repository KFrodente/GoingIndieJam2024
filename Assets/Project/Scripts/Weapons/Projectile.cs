using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int hits;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISION");
        if (other.TryGetComponent(out Damagable d) && d.GetImmunities() != (owner))
        {
            Debug.Log("DAMAGED");
            d.TakeDamage(po.damage);
            hits++;
            if(hits > po.pierceCount) Destroy(this.gameObject);
        }
    }
    private ProjectileObject po;

    private CharacterType owner;
    public void SetProjectile(ProjectileObject po, CharacterType _owner)
    {
        this.po = po;
        owner = _owner;
    }

    private float SpawnTime;

    private void Awake()
    {
        SpawnTime = Time.time;
    }


    protected void Update()
    {
        transform.position += transform.up * (Time.deltaTime * po.speed);
        if(po.lifetime > 0 && SpawnTime + po.lifetime > Time.time) Destroy(this.gameObject);
    }
}
