using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class DauntlessSpiritMovement : CharacterMovement
{
    [SerializeField] private BaseCharacter character;
    private Rigidbody2D rb;
    private bool turnAngle;

    private float transitionTime = 0.3f;
    private float transitionTimer;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

    private void Update()
    {
        TransitionTiming();
        //transform.position += transform.up * (character.statHandler.stats.MoveSpeed * Time.deltaTime);
	}

	private void FixedUpdate()
	{
        float turnMultiplier = 1;
        if(transitionTimer > 0) turnMultiplier = 3;
        // Turning
        transform.RotateAround(transform.position, Vector3.forward, turnMultiplier * character.statHandler.stats.TurnSpeed * Time.deltaTime * (turnAngle ? 1 : -1));

        // Dampening
        rb.velocity *= .95f;

        // Forward Movement
		rb.AddForce(transform.up * character.statHandler.stats.MoveSpeed, ForceMode2D.Force);


		//rb.velocity = Vector2.ClampMagnitude(rb.velocity, character.statHandler.stats.MaxMoveSpeed);
	}

    public override void LeftClickDown(Vector2 position)
    {

    }

    public override void Move(Vector2 direction)
    {
    }

    public override void RightClickDown(Vector2 position)
    {
        turnAngle = !turnAngle;
        transitionTimer = transitionTime;
    }

    private void TransitionTiming()
    {
        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
        }
    }


}
