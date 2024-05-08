using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPortal : Portal
{
    public BasicPortal(Transform connectedRoom) : base(connectedRoom)
    {
    }

    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
        character.transform.position = connectedTransform.position;
    }
}
