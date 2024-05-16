using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorvidAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] BaseCharacter c;
    AutoFireWeapon daggers;
    private void Start()
    {
        daggers = c.weapon as AutoFireWeapon;
    }
    private void Update()
    {
        animator.SetFloat("Speed", c.rb.velocity.magnitude);
        animator.SetBool("Attacking", daggers.attacking);
    }
}
