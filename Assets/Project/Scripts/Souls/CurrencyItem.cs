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

    private GameObject player;

    private void Awake()
    {
        rb.AddForce(Random.insideUnitCircle.normalized * 10, ForceMode2D.Force);
        player = FindObjectOfType<SpiritCharacter>().gameObject;
    }

    private void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = (direction).magnitude;
        float force = maxAttractionForce / distance;
        if(distance < maxAttractionDistance)
        {
            rb.AddForce(direction.normalized * force, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damagable damageable) && !damageable.IsEnemy)
        {
            // add currency based on a local value?
            SpiritCharacter.souls += soulValue;
            // remove from world
            Destroy(gameObject);
        }
    }
}
