using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OribtalMovement : CharacterMovement
{
    [SerializeField] private BaseCharacter character;
    private bool turnAngle;

	// public override void LeftClickDown(Vector2 position)
	// {
 //
	// }
 //
	// public override void Move(Vector2 direction)
 //    {
 //        
 //    }
 //
 //    public override void RightClickDown(Vector2 position)
 //    {
 //        turnAngle = !turnAngle;
 //    }

    private void Update()
    {
        // transform.position += transform.up * (character.statHandler.stats.MoveSpeed * Time.deltaTime);
        //
        // transform.RotateAround(transform.position, Vector3.forward, character.statHandler.stats.TurnSpeed * Time.deltaTime * (turnAngle ? 1 : -1));
    }
}
