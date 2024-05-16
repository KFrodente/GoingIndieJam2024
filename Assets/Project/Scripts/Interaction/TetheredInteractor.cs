using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredInteractor : Interactor
{
    public override void Add(Interactable i)
    {
        if (i is Portal) return;
        base.Add(i);
    }
}
