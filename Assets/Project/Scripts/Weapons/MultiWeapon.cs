using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiWeapon : Weapon
{
	[Header("Dont use weapon data above, only the ones filling the list")]
	[SerializeField] private List<Weapon> Weapons = new List<Weapon>();
	private Weapon currentWeapon;
	private Weapon prevWeapon;

	public override void InitializeCharacter(BaseCharacter c)
	{
		foreach (Weapon weapon in Weapons)
		{
			weapon.InitializeCharacter(c);
		}
		PickWeapon();
		bc = c;
	}

	public override bool StartAttack(Target target, BaseCharacter c)
	{
		if (!delayOver) return false;
		//savedTarget = target; 
		//if(prevWeapon) prevWeapon.EndAttack();
		
		currentWeapon.StartAttack(target, c);
		prevWeapon = currentWeapon;
		lastFireTime = Time.time;
		Debug.Log("DELAY delayOver: " + delayOver);
		PickWeapon();

		return true;
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
