using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProjectile : Projectile
{
    [SerializeField] protected float spin;
    protected override void Update()
    {
        if(projectileData.lifetime > 0 && Time.time > spawnTime + projectileData.lifetime ) DestroyProjectile();
        transform.RotateAround(transform.position, Vector3.forward, spin * Time.deltaTime);

    }
    
}
