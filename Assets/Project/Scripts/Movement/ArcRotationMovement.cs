using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ArcRotationMovement : ContinuousForwardMovement
{ 
	protected bool turnIndicator;

	protected void Update()
	{
		UpdateTimers();
		if (rotationFrozen) return;
		//SetTargetAngle(transform.up);
		//AngleTowardTargetAngle(savedCharacter.characterStats.stats.TurnSpeed, savedCharacter);
		transform.RotateAround(transform.position, Vector3.forward, savedCharacter.characterStats.stats.TurnSpeed * Time.deltaTime * (turnIndicator ? 1 : -1));
	}

	// protected void FixedUpdate()
	// {
	// }

	public void ChangeAngle()
	{
		turnIndicator = !turnIndicator;
	}
	
	
	
    // public override void RightClickDown(Vector2 position)
    // {
    //     turnAngle = !turnAngle;
    //     transitionTimer = transitionTime;
    // }

    // private void TransitionTiming()
    // {
    //     if (transitionTimer > 0)
    //     {
    //         transitionTimer -= Time.deltaTime;
    //     }
    // }

	// public override void Move(Vector2 direction)
	// {
	// 	
	// }
}