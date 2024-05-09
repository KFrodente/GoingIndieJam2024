using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContinuousForwardMovement : BaseMovement
{
	protected void FixedUpdate()
	{
        // Forward Movement
		Move(transform.up, savedCharacter.GetStats().MoveSpeed, ForceMode2D.Force, savedCharacter);
	}
}
