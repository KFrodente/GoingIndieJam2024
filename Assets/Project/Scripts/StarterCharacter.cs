using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterCharacter : MonoBehaviour
{
    [SerializeField] private SpiritCharacter spirit;
    [SerializeField] private Possessor possessor;
    [SerializeField] private Possessable possessable;

    private void Start()
    {
        possessable.OnInteract(spirit);
        BaseCharacter.playerCharacter = spirit;
    }
}
