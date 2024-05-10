using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damagable : MonoBehaviour
{
    [SerializeField] private bool isHitCounter;
    [SerializeField] private bool immuneToDamage;

    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private UnityEvent OnHit;

    public bool IsEnemy = true;

    private float immunityTimer = 0;

    // UI
    [SerializeField] Slider healthOverBar;
    [SerializeField] Slider healthUnderBar;

    // Health private vars
    [SerializeField] private int health = 3;
    private int startingHealth;

    private float lerpedHealth;
    private float lerpedHealthTarget;
    [SerializeField] private float healthLerpSpeed = 5f;

    public void ConvertToPlayer()
    {
        IsEnemy = false;
    }


    // Health Properties
    public int StartingHealth
    {
        get { return startingHealth; }
        set { startingHealth = value; }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthOverBar.value = health / (float)StartingHealth;
        }
    }

    
    private void Awake()
    {
        StartingHealth = Health;
        lerpedHealth = Health;
        lerpedHealthTarget = Health;
    }

    
    private void Update()
    {
        CountDownImmunity();
        if(applyDamage)
        {
            TakeDamage(damageToTake);
            applyDamage = false;
        }

        HealthBarLerping();
        
    }

    private void HealthBarLerping()
    {
        
        if(lerpedHealthTarget != lerpedHealth)
        {
            // when difference is greater, lerp slower, meaning lerp is more even
            float lerpTimeScalar = Mathf.Abs(lerpedHealth - lerpedHealthTarget);
            lerpedHealth = Mathf.Lerp(lerpedHealth, lerpedHealthTarget, (healthLerpSpeed / lerpTimeScalar) * Time.deltaTime);
            healthUnderBar.value = lerpedHealth / (float)StartingHealth;

            if (Mathf.Abs(lerpedHealth - lerpedHealthTarget) < 0.3)
            {
                lerpedHealth = lerpedHealthTarget;
            }
        }
    }

    
    public void StartImmunity(float time)
    {
        SetDamagable(false);
        if(immunityTimer < time) immunityTimer = time;
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
        Health -= damage;
        lerpedHealthTarget = Health;
        if (Health <= 0)
        {
            OnDeath?.Invoke();
        }
        else OnHit?.Invoke();
    }

    public void SetHealth(int hp)
    {
        Health = hp;
    }

    
    public void SetDamagable(bool isDamagable)
    {
        immuneToDamage = isDamagable;
    }

    public void RefillHealth()
    {
        SetHealth(startingHealth);
        
    }

    [Header("Damage Testing")]
    [SerializeField,Tooltip("Apply X damage")] int damageToTake = 0;
    [SerializeField,Tooltip("Toggle to inflict damage based on value above")] bool applyDamage = false;
}
