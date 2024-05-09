using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public Room connectedRoom;
    public int costToEnter;
    public bool paidPrice = false;

    public bool active;

    //Connect ALL portals after creating the portals

    public Portal(Room connectedRoom)
    {
        this.connectedRoom = connectedRoom;
    }

    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);
    }
}
