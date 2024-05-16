using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : Interactable
{
    
    [SerializeField] private PieceSeparation dragonMaker;
    private bool activated;
    
    public override void OnInteract(BaseCharacter character)
    {
        if(activated) return;
        Activate();

    }

    private void Activate()
    {
        activated = true;
        dragonMaker.SpawnDragon();
    }
}
