using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredSpiritCharacter : SpiritCharacter
{
    protected override void Update()
    {
        if(input.GetMouseInput().leftDown) Attack(new Target(true, null, transform.position, true));
        if(input.GetNormalizedMoveDirection() != Vector2.zero) movement.SetTargetAngle(input.GetNormalizedMoveDirection(), true);
        movement.AngleTowardTargetAngle(GetStats().MoveSpeed, this);
    }
}
