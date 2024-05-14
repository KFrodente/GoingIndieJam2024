using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suffocation : MonoBehaviour
{
    
    [SerializeField] private Damagable damager;
    [SerializeField] private float inWallTime;
    [SerializeField] private float suffocationTickDelay;
    private int inWallCount;
    private float timeWallEntered;
    private float timeSuffTick;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Tilemap"))
        {
            inWallCount++;
            timeWallEntered = Time.time;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Tilemap"))
        {
            inWallCount--;
        }
    }

    private void Update()
    {
        if (inWallCount > 0 && Time.time - timeWallEntered > inWallTime && Time.time - timeSuffTick > suffocationTickDelay)
        {
            Suffocate();
        }
    }
    private void Suffocate()
    {
        timeSuffTick = Time.time;
        damager.TakeDamage(1, ProjectileDamageType.Blunt);
    }
}
