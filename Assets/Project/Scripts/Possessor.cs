using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Possessor : Possessable
{
    [SerializeField] private Soul soul;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material canBePossessedMaterial;
    [SerializeField] private Material defaultMaterial;

    private void Awake()
    {
        if(spriteRenderer) defaultMaterial = spriteRenderer.material;
    }

    private List<Possessable> possessableObjects = new List<Possessable>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Possessable p))
        {
            possessableObjects.Add(p);
            if(spriteRenderer) spriteRenderer.material = canBePossessedMaterial;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out Possessable p))
        {
            possessableObjects.Remove(p);
            if(spriteRenderer) spriteRenderer.material = defaultMaterial;
        }
    }
    
    public void Possess()
    {
        if (possessableObjects.Count > 0)
        {
            Debug.Log("Possessed " + possessableObjects[0]);
            possessableObjects[Random.Range(0, possessableObjects.Count)].Possess(soul);
            possessableObjects.Clear();
            UnPossess(transform);
            
        }
    }
}
