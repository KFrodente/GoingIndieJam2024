using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class CorvidSpiritMovement : CharacterMovement
{
    [SerializeField] private BaseCharacter character;
    private Rigidbody2D rb;


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
        transform.RotateAround(transform.position, Vector3.forward, character.statHandler.stats.TurnSpeed * Time.deltaTime * angleMult);

        // Dampening
        rb.velocity *= .95f;

        // Forward Movement
		rb.AddForce(transform.up * character.statHandler.stats.MoveSpeed, ForceMode2D.Force);
	}

    public override void LeftClickDown(Vector2 position)
    {

    }

    public override void Move(Vector2 direction)
    {
        angleMult = -direction.x;
    }

    public override void RightClickDown(Vector2 position)
    {
        
    }



}
