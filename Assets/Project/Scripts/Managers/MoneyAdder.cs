using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAdder : MonoBehaviour
{
    [ContextMenu(nameof(AddMoney))]
    public void AddMoney()
    {
        SpiritCharacter.souls += 10;
    }
}
