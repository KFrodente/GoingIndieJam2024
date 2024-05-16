using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : Interactable
{
    
    [SerializeField] private PieceSeparation dragonMaker;
    [SerializeField] private GameObject particleActivation;
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
        if (particleActivation) particleActivation.SetActive(true);
    }
}
