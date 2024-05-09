using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPortal : Portal
{
    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
        //character.transform.position = connectedPortal.transform.position;
    }
}
