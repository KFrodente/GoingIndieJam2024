using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    protected List<Interactable> interactables = new List<Interactable>();
    protected Interactable lastInteractedWith;

    public virtual void Interact(BaseCharacter character)
    {
        if(interactables.Count > 0) GetClosest().OnInteract(character);
    }
    public virtual void StopInteract(BaseCharacter character)
    {
        if (lastInteractedWith == null) return;
        lastInteractedWith.StopInteract(character);
        lastInteractedWith = null;
    }

    public virtual void Add(Interactable i)
    {
        interactables.Add(i);
    }
    public virtual void Remove(Interactable i)
    {
        interactables.Remove(i);
    }

    protected Interactable GetClosest()
    {
        Interactable closest = null;
        float closestDist = float.MaxValue;
        foreach (Interactable i in interactables)
        {
            float dist = Vector2.Distance(transform.position, i.transform.position);
            if (dist < closestDist)
            {
                closest = i;
                closestDist = dist;
            }
        }
        return closest;
    }
}
