using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessCharacter : BaseCharacter
{
    protected override void Update()
    {
        if (input.GetMouseInput().leftDown)
        {
            Attack(input.GetInputTarget());
        }
        if(input.GetMouseInput().leftUp) weapon.EndAttack();
    }

    
}
