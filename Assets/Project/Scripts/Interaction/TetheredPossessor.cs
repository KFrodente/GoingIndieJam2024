using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredPossessor : Possessor
{
    [SerializeField] private TetheredDeadCharacter TDC;

    public override void Interact(BaseCharacter character)
    {
        if (interactables.Count > 0)
        {
            Interactable closest = GetClosest();
            Debug.Log("Does this even work?");
            Possess(closest, character);

        }
    }

    protected override void Possess(Interactable i, BaseCharacter c)
    {
        if (!(i is Possessable)) return;

        Debug.Log("SHould have sent over body");
        TDC.SetConnectedObject(i.transform.parent.gameObject);
        i.OnInteract(c);
    }
}
