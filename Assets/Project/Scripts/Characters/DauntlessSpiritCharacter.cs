using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessSpiritCharacter : SpiritCharacter
{
    [SerializeField] protected float turnSpeedUpMultiplier = 2;
    [SerializeField] protected float turnSpeedUpDuration = 0.2f;
    protected float transitionStartTime = 0;
    protected bool isInFastTurn => Time.time - transitionStartTime < turnSpeedUpDuration;
    protected override void Update()
    {
        if (input.GetMouseInput().rightDown)
        {
            movement.ChangeAngle(Vector2.zero);
            transitionStartTime = Time.time;
        }
        if(input.GetMouseInput().leftDown) Attack(new Target(true, null, transform.position, true));
        if(input.GetMouseInput().leftUp) weapon.EndAttack();
        if(isInFastTurn) movement.AngleTowardTargetAngle(turnSpeedUpMultiplier, this);
    }

    protected override void FixedUpdate()
    {
        
    }
    
}
