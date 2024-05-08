using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private List<Interactable> interactables = new List<Interactable>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable i))
        {
            interactables.Add(i);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable i))
        {
            interactables.Remove(i);
        }
    }

    public void Interact(BaseCharacter character)
    {
        if(interactables.Count > 0) GetClosest().OnInteract(character);
    }

    private Interactable GetClosest()
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
