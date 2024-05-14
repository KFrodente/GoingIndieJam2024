using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Projectile")]
public class ProjectileObject : ScriptableObject
{
    [Header("Basics")]
    public bool zeroDamage;
    public float speed;
    public float lifetime;
    public int pierceCount;
    public ProjectileDamageType type;
    
    [Header("Hit Effects")]
    public GameObject hitParticle;
    public AudioClip hitSound;


}
