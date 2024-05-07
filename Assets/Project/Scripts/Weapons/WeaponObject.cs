using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
public class WeaponObject : ScriptableObject
{
    public string weaponName;
    public AudioClip chargeSound;
    public float chargeTime;
    public float fireDelay;
    public AudioClip attackSound;
    public List<ProjectileObject> projectileObject;
    public ProjectilePattern pattern;
    

}


public enum ProjectilePattern
{
    Single,
    Random,
    Ordered,
    Other
}