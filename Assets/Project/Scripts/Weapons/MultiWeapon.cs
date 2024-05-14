using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

public class MultiWeapon : Weapon
{
	[Header("Dont use weapon data above, only the ones filling the list")]
	[SerializeField] private List<Weapon> Weapons = new List<Weapon>();
	private Weapon currentWeapon;
	private Weapon prevWeapon;

	[SerializeField] private WeaponSelectionType selectionType;
	[SerializeField] private List<int> weaponUsesBeforeSwap = new List<int>();
	private int alternateIndex = 0;
	private int currentTimesUsed = 0;

	public override void InitializeCharacter(BaseCharacter c)
	{
		foreach (Weapon weapon in Weapons)
		{
			weapon.InitializeCharacter(c);
		}
		PickWeapon();
		currentTimesUsed = 0;
		bc = c;
	}

	public override bool StartAttack(Target target, BaseCharacter c)
	{
		if (!delayOver) return false;
		//savedTarget = target; 
		//if(prevWeapon) prevWeapon.EndAttack();

		AttackWithCurrentWeapon(target, c);
		prevWeapon = currentWeapon;
		lastFireTime = Time.time;
		PickWeapon();

		return true;
	}

	private void AttackWithCurrentWeapon(Target t, BaseCharacter c)
	{
		switch (selectionType)
		{
			case WeaponSelectionType.Random :
			{
				
				break;
			}
			case WeaponSelectionType.Alternate:
			{
				currentTimesUsed++;
				Debug.Log("Current times used: " + currentTimesUsed + " out of " + weaponUsesBeforeSwap[alternateIndex] + " | " + currentWeapon);
				break;
			}
		}
		
		currentWeapon.StartAttack(t, c);
	}

	public override void EndAttack()
	{
		foreach (Weapon weapon in Weapons)
		{
			weapon.EndAttack();
		}
	}

	private void PickWeapon()
	{
		if(prevWeapon) prevWeapon.EndAttack();
		switch (selectionType)
		{
			case WeaponSelectionType.Random :
			{
				// get random to pick weapon
				int randomVal = Random.Range(0, Weapons.Count);

				// index into list with random number
				currentWeapon = Weapons[randomVal];
				break;
			}
			case WeaponSelectionType.Alternate:
			{
				if (currentTimesUsed >= weaponUsesBeforeSwap[alternateIndex])
				{
					alternateIndex++;
					currentTimesUsed = 0;
					if (alternateIndex >= Weapons.Count)
					{
						alternateIndex = 0;
						
					}
				}

				currentWeapon = Weapons[alternateIndex];
				Debug.Log(alternateIndex);
				break;
			}
		}
	}


	// Overrides all functionality because it will be handled by held weapons
	protected override void Fire(Target target)
	{
	}
	protected override void Start()
	{
	}
}

public enum WeaponSelectionType
{
	Random,
	Alternate
}
