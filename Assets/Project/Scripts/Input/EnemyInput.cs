using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    public static Transform playerCharacter;
    public override Vector2 GetNormalizedMoveDirection()
    {
        return (playerCharacter.position - transform.position).normalized;
    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = true, // Attack If Player In Some sort of range | Might need to have access to the baseCharacter to get stats
            leftUp = false,
            rightDown = false,
            rightUp = false,
            middleDown = false,
            middleUp = false
        };
    }
}
