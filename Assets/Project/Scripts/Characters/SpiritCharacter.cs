using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiritCharacter : BaseCharacter
{
    public static int souls = 0;

    public void Reliquish()
    {
        transform.SetParent(null);
        gameObject.SetActive(true);
    }
}
