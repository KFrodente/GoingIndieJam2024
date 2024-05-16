using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] BaseCharacter c;
    private ChargeWeapon bow;
    private void Start()
    {
        bow = c.weapon as ChargeWeapon;
    }
    private void Update()
    {
        animator.SetFloat("Speed", c.rb.velocity.magnitude);
        animator.SetBool("Attacking", bow.charging);
    }
}
