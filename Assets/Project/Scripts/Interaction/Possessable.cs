using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Possessable : Interactable
{
    [SerializeField] protected BaseInput playerInput;
    [SerializeField] protected BaseCharacter unitBase;
    [SerializeField] protected Interactor interactor;
    [SerializeField] protected Damagable damagable;
    public override void OnInteract(BaseCharacter character)
    {
        if (character is SpiritCharacter)
        {
            SwapInputs();
            SetPossession(character as SpiritCharacter);
        }
    }

    protected void SetPossession(SpiritCharacter c)
    {
        c.transform.SetParent(transform);
        c.transform.localPosition = Vector3.zero;
        c.gameObject.SetActive(false);
        unitBase.possessingSpirit = c;
        damagable.IsEnemy = false;
    }
    protected void SwapInputs()
    {
        unitBase.input = playerInput;
        interactor.gameObject.SetActive(true);
    }
}