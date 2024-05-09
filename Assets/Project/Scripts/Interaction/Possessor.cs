using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Possessor : Interactor
{
    public override void Interact(BaseCharacter character)
    {
        if (interactables.Count > 0)
        {
            Interactable closest = GetClosest();
            closest.OnInteract(character);
            Possess(closest);
            
        }
    }
    protected void Possess(Interactable i)
    {
        if (!(i is Possessable)) return;
        
    }
}
