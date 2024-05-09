using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ContinuousForwardMovement : CharacterMovement
{
	protected void FixedUpdate()
	{
        // Forward Movement
		Move(transform.up, savedCharacter.characterStats.stats.MoveSpeed, ForceMode2D.Force, savedCharacter);
	}
}
