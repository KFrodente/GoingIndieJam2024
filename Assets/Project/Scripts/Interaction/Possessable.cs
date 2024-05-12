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
        unitBase.weapon.EndAttack();
        c.transform.SetParent(transform);
        c.transform.localPosition = Vector3.zero;
        c.gameObject.SetActive(false);
        unitBase.possessingSpirit = c;
        BaseCharacter.playerCharacter = unitBase;
    }
    protected void SwapInputs()
    {
        unitBase.input = playerInput;
        interactor.gameObject.SetActive(true);
    }
}