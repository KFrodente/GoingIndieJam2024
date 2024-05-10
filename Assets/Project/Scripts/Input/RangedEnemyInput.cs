using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyInput : EnemyInput
{
    [SerializeField] protected float preferedRange; 
    public override Vector2 GetNormalizedMoveDirection()
    {
        
        return (GetDistance() > preferedRange ? 1 : -1) * (playerCharacter.position - transform.position).normalized;
    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = (GetDistance() < preferedRange),
            leftUp = false,
            rightDown = false,
            rightUp = false,
            middleDown = false,
            middleUp = false
        };
    }
    protected float GetDistance()
    {
        return Vector2.Distance(playerCharacter.position, transform.position);
    }
}
