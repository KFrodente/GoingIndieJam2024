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
    //[SerializeField] protected Transform spiritStorage;
    public override void OnInteract(BaseCharacter character)
    {
        SwapInputs();
        SetPossession(character);
    }

    protected void SetPossession(BaseCharacter c)
    {
        c.transform.SetParent(transform);
        c.transform.localPosition = Vector3.zero;
        c.gameObject.SetActive(false);
        unitBase.possessingSpirit = c;
    }
    protected void SwapInputs()
    {
        unitBase.input = playerInput;
        interactor.gameObject.SetActive(true);
    }
}