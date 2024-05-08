using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Possessor : Possessable
{
    [SerializeField] private SpiritSoul spiritSoul;
    

    
    private List<Possessable> possessableObjects = new List<Possessable>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Possessable p))
        {
            possessableObjects.Add(p);
            p.ChangeToPossessSprite();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out Possessable p))
        {
            possessableObjects.Remove(p);
            p.ChangeToUnpossessSprite();
        }
    }
    
    public void Possess()
    {
        if (possessableObjects.Count > 0)
        {
            Debug.Log("Possessed " + possessableObjects[0]);
            possessableObjects[Random.Range(0, possessableObjects.Count)].Possess(spiritSoul);
            possessableObjects.Clear();
            UnPossess(transform);
            
        }
    }
}
