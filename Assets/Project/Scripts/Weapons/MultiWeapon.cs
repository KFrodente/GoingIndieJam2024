using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiWeapon : Weapon
{
	[Header("Dont use weapon data above, only the ones filling the list")]
	[SerializeField] private List<AutoFireWeapon> Weapons = new List<AutoFireWeapon>();
	private AutoFireWeapon currentWeapon;
	private AutoFireWeapon prevWeapon;

	public override void InitializeCharacter(BaseCharacter c)
	{
		foreach (AutoFireWeapon weapon in Weapons)
		{
			weapon.InitializeCharacter(c);
		}
		PickWeapon();
	}

	public override bool StartAttack(Target target, BaseCharacter c)
	{
		if (prevWeapon && !prevWeapon.IsAutoFireDelayOver()) return false;

		if(currentWeapon.StartAttack(target, c))
		{
			prevWeapon = currentWeapon;
			PickWeapon();
		}

		return true;
	}

	public override void EndAttack()
	{
		currentWeapon.EndAttack();
	}

	private void PickWeapon()
	{
		// get random to pick weapon
		int randomVal = Random.Range(0, Weapons.Count);

		// index into list with random number
		currentWeapon = Weapons[randomVal];
	}


	// Overrides all functionality because it will be handled by held weapons
	protected override void Fire(Target target)
	{
	}
	protected override void Start()
	{
	}
}
