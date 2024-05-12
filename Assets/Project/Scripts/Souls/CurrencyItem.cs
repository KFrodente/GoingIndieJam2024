using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurrencyItem : MonoBehaviour
{
    [SerializeField] private int soulValue = 1;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float maxAttractionForce = 100;
    [SerializeField] private float maxAttractionDistance = 30;

    private bool pickupable = false;
    [SerializeField] private float pickupableDelay = 0.5f;
    
    private void Awake()
    {
        rb.AddForce(Random.insideUnitCircle.normalized * 50, ForceMode2D.Force);
        StartCoroutine(PickupableTimer());
    }

    private void FixedUpdate()
    {
        if(pickupable)
        {
            Vector2 direction = BaseCharacter.playerCharacter.transform.position - transform.position;
            float distance = (direction).magnitude;
            float force = maxAttractionForce / distance;
            if(distance < maxAttractionDistance)
            {
                rb.AddForce(direction.normalized * force, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickupable && collision.gameObject.TryGetComponent(out Damagable damageable) && damageable.IsPlayer)
        {
            // add currency based on a local value?
            BaseCharacter.playerCharacter.GainSouls(soulValue);
            // remove from world
            Destroy(gameObject);
        }
    }

    private IEnumerator PickupableTimer()
    {
        yield return new WaitForSeconds(pickupableDelay);
        pickupable = true;
    }
}
