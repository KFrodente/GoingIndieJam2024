using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SoulDispense : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0, 10)] private Vector2Int amountRange;
    [SerializeField] private GameObject soulPrefab;
    public void Dispense()
    {
        int amount = Random.Range(amountRange.x, amountRange.y);
        for (int i = 0; i < amount; i++)
        {
            Instantiate(soulPrefab, transform.position, Quaternion.identity);
        }
    }
}
