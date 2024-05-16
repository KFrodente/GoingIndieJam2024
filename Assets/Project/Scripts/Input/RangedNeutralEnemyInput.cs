using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedNeutralEnemyInput : RangedEnemyInput
{
    
    public override Target GetInputTarget()
    {
        return new Target(TargetType.Character, null, BaseCharacter.playerCharacter.transform, transform.position, false);

    }
    
}
