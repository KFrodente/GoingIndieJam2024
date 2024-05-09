using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public Portal connectedPortal;
    public int costToEnter;
    public bool paidPrice = false;

    public bool active;

    //Connect ALL portals after creating the portals

    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
        character.transform.position = connectedPortal.transform.position;
    }
}
