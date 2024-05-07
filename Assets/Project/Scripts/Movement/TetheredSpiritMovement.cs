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
    private float angleMult;


	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
	}

	private void FixedUpdate()
	{
        // Turning
        //transform.RotateAround(transform.position, Vector3.forward, character.statHandler.stats.TurnSpeed * Time.deltaTime * angleMult);
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * 180 / Mathf.PI - 90;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), character.statHandler.stats.TurnSpeed);

        // Dampening
        rb.velocity *= .95f;

        // Forward Movement
		rb.AddForce(moveDir);
	}


    public override void LeftClickDown(Vector2 position)
    {

    }

    public override void Move(Vector2 direction)
    {
        angleMult = -direction.x;
        moveDir = direction.normalized * character.statHandler.stats.MoveSpeed;
    }

    public override void RightClickDown(Vector2 position)
    {
        
    }



}
