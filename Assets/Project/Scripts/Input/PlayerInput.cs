using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public override Vector2 GetNormalizedMoveDirection()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = Input.GetMouseButtonDown(0),
            leftUp = Input.GetMouseButtonUp(0),
            rightDown = Input.GetMouseButtonDown(1),
            rightUp = Input.GetMouseButtonUp(1),
            middleDown = Input.GetMouseButtonDown(2),
            middleUp = Input.GetMouseButtonUp(2)
        };
    }
    public override Target GetInputTarget()
    {
        return new Target(TargetType.Mouse, null, null, transform.position, true);
    }
}
