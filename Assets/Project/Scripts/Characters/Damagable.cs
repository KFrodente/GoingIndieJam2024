using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private CharacterType immunities;
    
    public void SetImmunities(int i)
    {
        immunities = (CharacterType)i;
    }
    public CharacterType GetImmunities()
    {
        return immunities;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void SetHealth(int hp)
    {
        health = hp;
        
    }

    private void Update()
    {
        
    }
}

public enum CharacterType
{
    None = 0,
    Player = 1,
    Enemy = 2
}