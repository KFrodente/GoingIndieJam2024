using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private StatEffect onHitEffect;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip hitParticle;
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Damagable d) && d.IsPlayer)
        {
            d.TakeDamage(damage, ProjectileDamageType.Blunt);
            if(onHitEffect != null) d.baseCharacter.characterStats.AddStatModifier(onHitEffect.GetModifier());
            if(hitSound != null) AudioManager.instance.Play(hitSound);
            if(hitParticle != null) Instantiate(hitParticle, transform.position, transform.rotation);
        }
    }
}
