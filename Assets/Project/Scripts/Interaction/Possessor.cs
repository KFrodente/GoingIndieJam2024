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
            Possess(closest, character);
            
        }
    }
    protected virtual void Possess(Interactable i, BaseCharacter c)
    {
        if (!(i is Possessable)) return;
        i.OnInteract(c);
        // Code for counting Kian
    }
}
