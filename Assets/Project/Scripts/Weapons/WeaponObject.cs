using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
public class WeaponObject : ScriptableObject
{
    public string weaponName;
    public WeaponType type;
    public AudioClip chargeSound;
    public float chargeTime;
    public AudioClip attackSound;
    public List<ProjectileObject> projectileObject;
    public ProjectilePattern pattern;


}

public enum WeaponType
{
    Melee,
    Ranged
}

public enum ProjectilePattern
{
    Single,
    RoundRobin,
    Random,
    Ordered,
    Other
}