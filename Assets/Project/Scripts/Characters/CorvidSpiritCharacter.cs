using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvidSpiritCharacter : SpiritCharacter
{
    protected override void Update()
    {
        movement.ChangeAngle(input.GetNormalizedMoveDirection());
        if(input.GetMouseInput().leftDown) Attack(input.GetInputTarget());
    }

    protected override void FixedUpdate()
    {
        
    }
    
}
