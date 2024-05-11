using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("Just a test script to add money. DO NOT USE")]
public class MoneyAdder : MonoBehaviour
{
    SpiritCharacter character;
    private void Start()
    {
        character = FindFirstObjectByType<SpiritCharacter>();
    }

    [ContextMenu(nameof(Add10Money))]
    public void Add10Money()
    {
        character.Souls += 10;
    }

    [ContextMenu(nameof(Add20Money))]
    public void Add20Money()
    {
        character.Souls += 20;
    }

    [ContextMenu(nameof(Add30Money))]
    public void Add30Money()
    {
        character.Souls += 30;
    }

    [ContextMenu(nameof(Add40Money))]
    public void Add40Money()
    {
        character.Souls += 40;
    }

    [ContextMenu(nameof(Add50Money))]
    public void Add50Money()
    {
        character.Souls += 50;
    }
}
