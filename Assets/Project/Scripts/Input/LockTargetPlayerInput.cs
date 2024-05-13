using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTargetPlayerInput : PlayerInput
{
    public override Target GetInputTarget()
    {
        return new Target(TargetType.Position, InputUtils.GetMousePosition(), null, transform.position, true);
    }
}
