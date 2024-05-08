using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredSpiritWeapon : Weapon
{
	[SerializeField] private BaseCharacter character;

	public override void StartAttack(Vector2 target)
	{
		base.StartAttack(target);
	}
}
