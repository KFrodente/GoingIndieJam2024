using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : Weapon
{
	public bool charging;
	protected bool isChargingOver => Time.time - startChargeTime > weaponData.chargeTime;
	protected float startChargeTime = 0;
	protected bool isCancelOver => Time.time - startCancelTime > weaponData.cancelDelay;
	protected float startCancelTime = 0;
	public override bool StartAttack(Target target, BaseCharacter c)
	{
		if (!delayOver || !isCancelOver || charging) return false;
		bc = c;
		savedTarget = target;
		startChargeTime = Time.time;
		charging = true;
		return true;
	}
	
	protected virtual void Start()
	{
		lastFireTime = -bc.GetStats().AttackSpeed;
		startCancelTime = -weaponData.cancelDelay;
	}

	protected virtual void Update()
	{
		if(charging && isChargingOver)
		{
			charging = false;
			Fire(savedTarget);
		}
	}
	
}