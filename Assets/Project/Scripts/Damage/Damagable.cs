using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damagable : MonoBehaviour
{
    [Header("Damage Properties")]
    protected int startingHealth;
    [SerializeField] protected int health = 3;
    [SerializeField] protected bool isHitCounter;
    [SerializeField] protected bool alwaysImmune;
    [SerializeField] protected bool isSpirit;

    [Header("Character Data")]
    [SerializeField] protected BaseCharacter baseCharacter;
    [SerializeField] protected DamageAudioPlayer damageAudioPlayer;
    [SerializeField] protected SoulDispense dispenser;

    // UI
    [Header("UI")]
    [SerializeField] Slider healthOverBar;
    [SerializeField] Slider healthUnderBar;

    // Health protected vars
    [SerializeField] protected float healthLerpSpeed = 5f;
    protected float lerpedHealth;
    protected float lerpedHealthTarget;

    public virtual bool IsPlayer => baseCharacter.possessingSpirit != null || isSpirit;

    protected float immunityEndTime = 0;
    protected bool isImmune => Time.time < immunityEndTime || alwaysImmune; 
    
    // Health Properties
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if(healthOverBar != null) healthOverBar.value = health / (float)startingHealth;
        }
    }
    
    protected void Awake()
    {
        startingHealth = Health;
        lerpedHealth = Health;
        lerpedHealthTarget = Health;
    }

    
    protected void Update()
    {
        UpdateHealthBars();
    }

    protected void UpdateHealthBars()
    {
        
        if(lerpedHealthTarget != lerpedHealth)
        {
            // when difference is greater, lerp slower, meaning lerp is more even
            float lerpTimeScalar = Mathf.Abs(lerpedHealth - lerpedHealthTarget);
            lerpedHealth = Mathf.Lerp(lerpedHealth, lerpedHealthTarget, (healthLerpSpeed / lerpTimeScalar) * Time.deltaTime);
            healthUnderBar.value = lerpedHealth / startingHealth;

            if (Mathf.Abs(lerpedHealth - lerpedHealthTarget) < 0.3)
            {
                lerpedHealth = lerpedHealthTarget;
            }
        }
    }

    
    public void GainImmunity(float time)
    {
        if (immunityEndTime - Time.time < time)
        {
            immunityEndTime = Time.time + time;
        }
    }
    
    public virtual void TakeDamage(int damage)
    {
        if (isImmune) return;
        if(isHitCounter) damage = 1;
        Health -= damage;
        lerpedHealthTarget = Health;
        if (Health <= 0)
        {
            Die();
        }
        else Hurt();
    }
    protected virtual void Die()
    {
        damageAudioPlayer.PlayKilledSound();
        if(dispenser != null) dispenser.Dispense();
        if(IsPlayer && baseCharacter.possessingSpirit != null) baseCharacter.possessingSpirit.Reliquish();
        Destroy(baseCharacter.gameObject);
    }

    protected virtual void Hurt()
    {
        damageAudioPlayer.PlayHitSound();
    }
    
    [Button]
    public virtual void ApplyDamage()
    {
        TakeDamage(testDamageAmount);
    }
    [SerializeField,Tooltip("Apply X damage"),Header("Testing")] int testDamageAmount = 5;
}