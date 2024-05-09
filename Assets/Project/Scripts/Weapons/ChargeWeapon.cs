using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : Weapon
{
	protected bool charging;
	protected bool isChargingOver => Time.time - startChargeTime > weaponData.chargeTime;
	protected float startChargeTime = 0;
	public override void StartAttack(Target target, BaseCharacter c)
	{
		bc = c;
		savedTarget = target;
		startChargeTime = Time.time;
		charging = true;
	}

	protected virtual void Update()
	{
		if(charging && isChargingOver)
		{
			charging = false;
			Fire(savedTarget.GetDirection(), savedTarget.playerTargeting);
		}
	}
	
}