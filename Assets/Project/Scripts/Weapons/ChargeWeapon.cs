using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : Weapon
{
	protected float chargeTimer;
	protected bool charging;

	protected void Update()
	{
		if(charging)
		{
			chargeTimer -= Time.deltaTime;
			if(chargeTimer <= 0)
			{
				charging = false;
				DoChargeAttack();
			}
		}
	}

	public override void StartAttack(Vector2 target)
	{
		base.StartAttack(target);
		chargeTimer = weapon.chargeTime;
		charging = true;
	}

	public virtual void DoChargeAttack()
	{
	}
}