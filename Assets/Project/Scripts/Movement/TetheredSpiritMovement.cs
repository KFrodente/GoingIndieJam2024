using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class TetheredSpiritMovement : CharacterMovement
{
    [SerializeField] private BaseCharacter character;
    private Rigidbody2D rb;
    Vector2 moveDir;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
  //       // Turning
  //       float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * 180 / Mathf.PI - 90;
  //       transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), character.statHandler.stats.TurnSpeed);
  //
  //       // Dampening
  //       rb.velocity *= .95f;
  //
  //       // Forward Movement
		// rb.AddForce(moveDir);
	}

    // public override void Move(Vector2 direction)
    // {
    //     //moveDir = direction.normalized * character.statHandler.stats.MoveSpeed;
    // }
}
