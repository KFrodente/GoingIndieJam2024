using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    private int startingHealth;
    [SerializeField] private bool isHitCounter;
    [SerializeField] private bool immuneToDamage;
    [SerializeField] private int health = 3;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent OnHit;

    private float immunityTimer = 0;

    private void Awake()
    {
        startingHealth = health;
    }

    
    private void Update()
    {
        CountDownImmunity();
    }

    public void SetImmunities(int i)
    {
        immunities = (CharacterType)i;
    }
    public CharacterType GetImmunities()
    {
        return immunities;
    }

    public void StartImmunity(float time)
    {
        SetDamagable(false);
        immunityTimer = time;
    }
    private void CountDownImmunity()
    {
        if (immunityTimer > 0)
        {
            immunityTimer -= Time.deltaTime;
            if (immunityTimer <= 0)SetDamagable(true);
        }
    }

    public void TakeDamage(int damage)
    {
        if (immuneToDamage) return;
        if(isHitCounter) damage = 1;
        health -= damage;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
        else OnHit?.Invoke();
    }

    public void SetHealth(int hp)
    {
        health = hp;
    }

    public void SetDamagable(bool isDamagable)
    {
        immuneToDamage = isDamagable;
    }

    public void RefillHealth()
    {
        SetHealth(startingHealth);
        
    }
}
