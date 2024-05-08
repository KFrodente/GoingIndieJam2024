using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurrencyItem : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        rb.AddForce(Random.insideUnitCircle.normalized * 10, ForceMode2D.Force);
    }
}
