using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Interactor i = other.GetComponentInChildren<Interactor>();
        if (i != null)
        {
            i.Add(this);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        Interactor i = other.GetComponentInChildren<Interactor>();
        if (i != null)
        {
            i.Remove(this);
        }
    }
    public virtual void OnInteract(BaseCharacter character)
    {
        
    }
    public virtual void StopInteract(BaseCharacter character)
    {
        
    }
}
