using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Projectile")]
public class ProjectileObject : ScriptableObject
{
    public string projectileName;
    public GameObject projectileObject;
    public GameObject hitParticle;
    public AudioClip hitSound;
    public int pierceCount;


}
