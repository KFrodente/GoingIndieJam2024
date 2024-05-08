using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public Transform connectedTransform;
    public int costToEnter;
    public bool paidPrice;

    public Portal(Transform connectedRoom)
    {
        connectedTransform = connectedRoom;
    }

    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
    }
}
