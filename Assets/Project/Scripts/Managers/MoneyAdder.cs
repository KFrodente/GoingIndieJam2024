using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("Just a test script to add money. DO NOT USE")]
public class MoneyAdder : MonoBehaviour
{
    [ContextMenu(nameof(Add10Money))]
    public void Add10Money()
    {
        SpiritCharacter.souls += 10;
    }

    [ContextMenu(nameof(Add20Money))]
    public void Add20Money()
    {
        SpiritCharacter.souls += 20;
    }

    [ContextMenu(nameof(Add30Money))]
    public void Add30Money()
    {
        SpiritCharacter.souls += 30;
    }

    [ContextMenu(nameof(Add40Money))]
    public void Add40Money()
    {
        SpiritCharacter.souls += 40;
    }

    [ContextMenu(nameof(Add50Money))]
    public void Add50Money()
    {
        SpiritCharacter.souls += 50;
    }
}
