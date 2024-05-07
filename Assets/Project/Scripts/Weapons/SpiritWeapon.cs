using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritWeapon : Weapon
{
	[SerializeField] protected BaseCharacter character;

	public override void StartAttack(Vector2 target)
	{
		base.StartAttack(target);
	}
}
