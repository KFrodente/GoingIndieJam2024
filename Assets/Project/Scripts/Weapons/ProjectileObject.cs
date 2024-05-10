using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Projectile")]
public class ProjectileObject : ScriptableObject
{
    [Header("Basics")]
    public float damage;
    public float speed;
    public float lifetime;
    public int pierceCount;
    
    [Header("Hit Effects")]
    public GameObject hitParticle;
    public AudioClip hitSound;


}
