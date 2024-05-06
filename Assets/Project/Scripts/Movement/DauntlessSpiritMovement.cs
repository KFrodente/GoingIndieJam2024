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

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public override void LeftClick(Vector2 position)
	{
		rb.AddForce(transform.up * 30, ForceMode2D.Impulse);
	}

	public override void Move(Vector2 direction)
    {
        //rb.AddForce(transform.up*10, ForceMode2D.Impulse);
    }

    public override void RightClick(Vector2 position)
    {
        turnAngle = !turnAngle;
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, character.statHandler.stats.TurnSpeed * Time.deltaTime * (turnAngle ? 1 : -1));

		transform.position += transform.up * (character.statHandler.stats.MoveSpeed * Time.deltaTime);
	}

	private void FixedUpdate()
	{
		rb.velocity *= .95f;
		//rb.AddForce(transform.up * character.statHandler.stats.MoveSpeed, ForceMode2D.Force);
		//rb.velocity = Vector2.ClampMagnitude(rb.velocity, character.statHandler.stats.MaxMoveSpeed);
	}


}
