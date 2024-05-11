using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyInput : EnemyInput
{
    [SerializeField] protected float preferedRange; 
    public override Vector2 GetNormalizedMoveDirection()
    {
        if (BaseCharacter.playerCharacter.transform == null) return Vector2.zero;
        return (GetDistance() > preferedRange ? 1 : -1) * (BaseCharacter.playerCharacter.transform.position - transform.position).normalized;
    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = (GetDistance() < preferedRange * 1.5f), // Might want to get actual attack range from stats
            leftUp = (GetDistance() > preferedRange * 1.5f),
            rightDown = false,
            rightUp = false,
            middleDown = false,
            middleUp = false
        };
    }
    protected float GetDistance()
    {
        if (BaseCharacter.playerCharacter.transform == null) return 0;
        return Vector2.Distance(BaseCharacter.playerCharacter.transform.position, transform.position);
    }
}
