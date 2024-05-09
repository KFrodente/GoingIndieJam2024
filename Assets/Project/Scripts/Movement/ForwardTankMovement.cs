using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ForwardTankMovement : ArcRotationMovement
{ 

	protected void Update()
	{
		UpdateTimers();
		if (rotationFrozen) return;
		transform.RotateAround(transform.position, Vector3.forward, savedCharacter.GetStats().TurnSpeed * Time.deltaTime * turnAngle);
	}

	public override void ChangeAngle(Vector2 direction)
	{
		turnAngle = -direction.x;
	}
}
