using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPortal : Portal
{
    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
        Debug.Log("interacted with portal!");
        character.transform.position = connectedPortal.transform.position;
    }
}
