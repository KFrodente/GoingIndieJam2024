using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public static Transform playerCharacter;
    public override Vector2 GetNormalizedMoveDirection()
    {
        if (playerCharacter == null) return Vector2.zero;
        return (playerCharacter.position - transform.position).normalized;
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

    public override Transform GetInputTarget()
    {
        if (playerCharacter != null) return playerCharacter;
        return null;
    }
}
