using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private UnityEvent OnDeath;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    private void Update()
    {
        
    }
}
