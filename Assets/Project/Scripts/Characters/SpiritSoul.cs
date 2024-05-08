using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpiritSoul : BaseCharacter
{
    [SerializeField] private UnityEvent OnReanimate;
    public void Reanimate(Transform character)
    {
        transform.position = character.position;
        OnReanimate?.Invoke();
    }
}
