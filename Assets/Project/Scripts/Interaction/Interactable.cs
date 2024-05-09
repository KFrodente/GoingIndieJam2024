using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactor i))
        {
            i.Add(this);
        }
    }
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactor i))
        {
            i.Remove(this);
        }
    }
    public virtual void OnInteract(BaseCharacter character)
    {
        
    }
}
