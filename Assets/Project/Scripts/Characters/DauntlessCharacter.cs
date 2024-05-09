using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessCharacter : BaseCharacter
{
    protected override void Update()
    {
        if(input.GetMouseInput().rightDown && movement is ArcRotationMovement) (movement as ArcRotationMovement).ChangeAngle(Vector2.zero);
        if(input.GetMouseInput().leftDown) Attack(new Target(true, null, transform.position, true));
        if(input.GetMouseInput().leftUp) weapon.EndAttack();
    }

    
}
