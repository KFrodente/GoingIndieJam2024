using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiritCharacter : BaseCharacter
{
    public void Reliquish()
    {
        transform.SetParent(null);
        gameObject.SetActive(true);
    }
    [SerializeField] IntEvent SoulsUpdated = default;

    public int souls = 0;

    public int Souls
    {
        get { return souls; }
        set { 
            souls = value;
            SoulsUpdated.RaiseEvent(value);
        }
    }

    private void Awake()
    {
        SoulsUpdated.RaiseEvent(0);
    }
}
