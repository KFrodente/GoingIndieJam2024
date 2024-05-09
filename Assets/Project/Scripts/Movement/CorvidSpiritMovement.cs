using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class CorvidSpiritMovement : CharacterMovement
{
    [SerializeField] private BaseCharacter character;

    private float angleMult;

	private void FixedUpdate()
	{
        // Turning
  //       transform.RotateAround(transform.position, Vector3.forward, character.statHandler.stats.TurnSpeed * Time.deltaTime * angleMult);
  //
  //       // Dampening
  //       character.rb.velocity *= .95f;
  //
  //       // Forward Movement
		// character.rb.AddForce(transform.up * character.statHandler.stats.MoveSpeed, ForceMode2D.Force);
	}

    public override void Move(Vector2 direction, float speed, ForceMode2D forceMode, BaseCharacter c, bool forcedAction = false)
    {
        angleMult = -direction.x;
    }
}
