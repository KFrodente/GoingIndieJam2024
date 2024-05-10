using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
public class WeaponObjectData : ScriptableObject
{
    [Header("Basics")]
    public float fireDelay;
    public AudioClip attackSound;
    [Header("Charge")]
    public float chargeTime;
    public float cancelDelay;
    public AudioClip chargeSound;
    [Header("Burst")]
    public float burstSeparationDelay;
    public int burstAmount;
    [Header("Other")]
    public float attackDuration;
    public float knockback;
    [Header("Projectile")]
    public Projectile projectile;
    

}
