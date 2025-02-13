using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvidCharacter : BaseCharacter
{
    protected override void Update()
    {
        if(input.GetMouseInput().rightDown) movement.ChangeAngle(Vector2.zero);
        if(input.GetMouseInput().leftDown) Attack(input.GetInputTarget());
        if(input.GetMouseInput().leftUp) weapon.EndAttack();
        xFlipping();
    }

    
}
