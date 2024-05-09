using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessSpiritCharacter : SpiritCharacter
{
    protected override void Update()
    {
        if(input.GetMouseInput().rightDown && movement is ArcRotationMovement) (movement as ArcRotationMovement).ChangeAngle();
        if(input.GetMouseInput().leftDown) Attack(new Target(true, null, transform.position, true));
        if(input.GetMouseInput().leftUp) weapon.EndAttack();
    }

    protected override void FixedUpdate()
    {
        
    }
    
}
