using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNeutralEnemyInput : RangedEnemyInput
{
    
    public override Target GetInputTarget()
    {
        return new Target(TargetType.Character, null, BaseCharacter.playerCharacter.transform, transform.position, false);

    }
    public override MouseInputData GetMouseInput()
    {
        return new MouseInputData
        {
            leftDown = (GetDistance() < character.GetStats().AttackRange * 3),
            //leftDown = (GetDistance() < preferedRange * 1.5f), // Might want to get actual attack range from stats
            leftUp = (GetDistance() > character.GetStats().AttackRange * 3),
            //leftUp = (GetDistance() > preferedRange * 1.5f),
            rightDown = false,
            rightUp = false,
            middleDown = false,
            middleUp = false
        };
    }
    
}
