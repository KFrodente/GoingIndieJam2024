using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class DauntlessSpiritMovement : CharacterMovement
{
    private bool turnAngle;

    private float transitionTime = 0.3f;
    private float transitionTimer;

    private bool overrideRotation = false;

    private void Update()
    {
        TransitionTiming();
	}

	private void FixedUpdate()
	{
  //       float turnMultiplier = 1;
  //       if(transitionTimer > 0) turnMultiplier = 3;
  //       // Turning
  //       if(!overrideRotation) transform.RotateAround(transform.position, Vector3.forward, turnMultiplier * character.statHandler.stats.TurnSpeed * Time.deltaTime * (turnAngle ? 1 : -1));
  //
  //       // Dampening
  //       character.rb.velocity *= .95f;
  //
  //       // Forward Movement
		// character.rb.AddForce(transform.up * character.statHandler.stats.MoveSpeed, ForceMode2D.Force);
	}

    // public override void RightClickDown(Vector2 position)
    // {
    //     turnAngle = !turnAngle;
    //     transitionTimer = transitionTime;
    // }

    private void TransitionTiming()
    {
        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
        }
    }

	// public override void Move(Vector2 direction)
	// {
	// 	
	// }
}
