using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
public class WeaponObjectData : ScriptableObject
{
    public string weaponName;
    public AudioClip chargeSound;
    public float chargeTime;
    public float fireDelay;
    public float burstSeparationDelay;
    public int burstAmount;
    public float attackDuration;
    public float knockback;
    public AudioClip attackSound;
    public List<ProjectileObject> projectileObject;
    public ProjectilePattern pattern;
    

}


public enum ProjectilePattern
{
    Single,
    Random,
    Ordered,
    Shape,
    Circle,
    Other
}