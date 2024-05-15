using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DauntlessCharacter : BaseCharacter
{
    DashChargeWeapon chargeWeapon;
    protected override void Awake()
    {
        base.Awake();
        chargeWeapon = weapon as DashChargeWeapon;
    }

    [SerializeField] Animator animator;
    protected override void Update()
    {
        xFlipping();
        animator.SetFloat("Speed", rb.velocity.magnitude);
        animator.SetBool("Charging", chargeWeapon.inDash);
        animator.SetBool("PreppingCharge", chargeWeapon.charging);
        if (chargeWeapon.charging)
        {
            overrideFlipping = true;
            gfx.flipX = InputUtils.GetMousePosition().x < transform.position.x;
        }
        else overrideFlipping = false;
        if (input.GetMouseInput().leftDown)
        {
            Attack(input.GetInputTarget());
        }
        if(input.GetMouseInput().leftUp) weapon.EndAttack();
    }

    
}
