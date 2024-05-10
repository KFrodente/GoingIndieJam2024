using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("Just a test script to add money. DO NOT USE")]
public class MoneyAdder : MonoBehaviour
{
    [ContextMenu(nameof(AddMoney))]
    public void AddMoney()
    {
        SpiritCharacter.souls += 10;
    }
}
