using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    [SerializeField] protected float clickRate = 0.5f;
    protected float lastClickTime = 0;
    public override Vector2 GetNormalizedMoveDirection()
    {
        if (BaseCharacter.playerCharacter.transform == null) return Vector2.zero;
        return (BaseCharacter.playerCharacter.transform.position - transform.position).normalized;
    }
    public override MouseInputData GetMouseInput()
    {
        bool clicked = Time.time - lastClickTime > clickRate;
        if(clicked) lastClickTime = Time.time;
        return new MouseInputData
        {
            leftDown = clicked,
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
