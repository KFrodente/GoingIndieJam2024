using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public override Vector2 GetNormalizedMoveDirection()
    {
        if (BaseCharacter.playerCharacter.transform == null) return Vector2.zero;
        return (BaseCharacter.playerCharacter.transform.position - transform.position).normalized;
    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = true, // Spamming??
            leftUp = false,
            rightDown = false,
            rightUp = false,
            middleDown = false,
            middleUp = false
        };
    }

    public override Target GetInputTarget()
    {
        if (BaseCharacter.playerCharacter.transform != null) return new Target(TargetType.Character, null, BaseCharacter.playerCharacter.transform, transform.position, false);
        return null;
    }
}
