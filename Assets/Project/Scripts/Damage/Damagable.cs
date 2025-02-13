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
    [SerializeField] protected float spiritEjectForce = 100;
    [Header("Damage Properties")]
    public int startingHealth;
    [SerializeField] protected int health = 3;
    [SerializeField] protected bool isHitCounter;
    [SerializeField] protected bool alwaysImmune;
    [SerializeField] protected bool isSpirit;
    [SerializeField] protected ProjectileDamageType immunities;

    [Header("Character Data")]
    [SerializeField] public BaseCharacter baseCharacter;
    [SerializeField] protected DamageAudioPlayer damageAudioPlayer;
    [SerializeField] protected SoulDispense dispenser;

    // UI
    [Header("UI")]
    [SerializeField] protected Slider healthOverBar;
    [SerializeField] protected Slider healthUnderBar;

    // Health protected vars
    [SerializeField] protected float healthLerpSpeed = 20f;
    protected float lerpedHealth;
    protected float lerpedHealthTarget;


    [HideInInspector] public bool pointsSubtracted = false;
    public virtual bool IsPlayer => baseCharacter.possessingSpirit != null || isSpirit;

    protected float immunityEndTime = 0;

    protected bool isImmune(ProjectileDamageType t)
    {
        return Time.time < immunityEndTime || alwaysImmune || immunities.HasFlag(t);
    }

    // Health Properties
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health > startingHealth) health = startingHealth;
            if(healthOverBar != null) healthOverBar.value = health / (float)startingHealth;
        }
    }
    
    protected void Awake()
    {
        startingHealth = Health;
        lerpedHealth = Health;
        lerpedHealthTarget = Health;
    }

    
    protected virtual void Update()
    {
        UpdateHealthBars();
    }

    protected void UpdateHealthBars()
    {

		if (lerpedHealthTarget != lerpedHealth) { 
		//{
		//	// when difference is greater, lerp slower, meaning lerp is more even
		//	float lerpTimeScalar = Mathf.Abs(lerpedHealth - lerpedHealthTarget);
		//	lerpedHealth = Mathf.Lerp(lerpedHealth, lerpedHealthTarget, (healthLerpSpeed / lerpTimeScalar) * Time.deltaTime);
		//	healthUnderBar.value = lerpedHealth / startingHealth;
		//	//float lerpTimeScalar = Mathf.Abs(lerpedHealth - lerpedHealthTarget);
		//	//lerpedHealth = Mathf.Lerp(lerpedHealth, lerpedHealthTarget, (healthLerpSpeed / lerpTimeScalar) * Time.deltaTime);

			if (lerpedHealth > lerpedHealthTarget)
			{
				lerpedHealth -= healthLerpSpeed * Time.deltaTime;
			}
			else // less than
			{
				lerpedHealth += healthLerpSpeed * Time.deltaTime;
			}

			if (Mathf.Abs(lerpedHealth - lerpedHealthTarget) < 0.3)
			{
				lerpedHealth = lerpedHealthTarget;
			}

			if(healthUnderBar) healthUnderBar.value = lerpedHealth / startingHealth;
		}
	}



    
    public void GainImmunity(float time)
    {
        if (immunityEndTime - Time.time < time)
        {
            immunityEndTime = Time.time + time;
        }
    }
    
    public virtual bool TakeDamage(int damage, ProjectileDamageType type)
    {
        if (isImmune(type)) return false;
        if(isHitCounter) damage = 1;
        Health -= damage;
        lerpedHealthTarget = Health;
        if (Health <= 0)
        {
            Die();
        }
        else Hurt();
        return true;
    }

    
    public virtual void Die(bool suicide = false)
    {
        if(damageAudioPlayer) damageAudioPlayer.PlayKilledSound();
        if(!suicide && dispenser != null) dispenser.Dispense();
        if (IsPlayer && baseCharacter.possessingSpirit != null)
        {
            if(this.gameObject.CompareTag("Spectre"))
            {
				StartCoroutine(TransitionManager.instance.FadeToBlack());
				StartCoroutine(TransitionManager.instance.SlideUpButton());
				TransitionManager.instance.TypeText2();
                CharacterSelectManager.selectedCharacter = CharacterSelectManager.Characters.None;
			}
            if(baseCharacter.isSpirit)
            { // LOSE!!!
                AudioManager.instance.doAudio = false;
                StartCoroutine(TransitionManager.instance.FadeToBlack());
                StartCoroutine(TransitionManager.instance.SlideUpButton());
                TransitionManager.instance.TypeText();
                CharacterSelectManager.selectedCharacter = CharacterSelectManager.Characters.None;
                this.enabled = false;
                return;
            }
            else
            { // Eject spirit

                baseCharacter.possessingSpirit.Reliquish();
                if (baseCharacter.possessingSpirit.TryGetComponent(out Damagable d))
                {
                    d.GainImmunity(1f);
                }
                if (baseCharacter.possessingSpirit.TryGetComponent(out Rigidbody2D rb))
                {
                    rb.AddForce(baseCharacter.possessingSpirit.transform.up * spiritEjectForce, ForceMode2D.Force);
                }
            }
        }
        if (transform.parent != null)
        {
            if (transform.parent.TryGetComponent(out EnemySpawner ES) && !pointsSubtracted)
            {
                ES.spawnedEnemies--;
                pointsSubtracted = true;
            }
        }
        Destroy(baseCharacter.gameObject);
    }



    protected virtual void Hurt()
    {
        if(damageAudioPlayer) damageAudioPlayer.PlayHitSound();
        GainImmunity(0.15f);
    }

    public virtual float GetHealthPercent()
    {
        return health / (float)startingHealth;
    }
    
    [Button]
    public virtual void ApplyDamage()
    {
        TakeDamage(testDamageAmount, ProjectileDamageType.Blunt);
    }
    [SerializeField,Tooltip("Apply X damage"),Header("Testing")] int testDamageAmount = 5;

    public virtual void RefillHealth()
    {
        //immunityEndTime = 0;
        Health = startingHealth;
    }
}

