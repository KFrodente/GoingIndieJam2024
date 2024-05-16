using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obelisk : Interactable
{
    
    [SerializeField] private PieceSeparation dragonMaker;
    
    public override void OnInteract(BaseCharacter character)
    {
        dragonMaker.SpawnDragon();
    }
}
