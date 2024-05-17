using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemistAnimationHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] BaseCharacter c;
    private MultiWeapon potions;
    private void Start()
    {
        potions = c.weapon as MultiWeapon;
    }
    private void Update()
    {
        animator.SetFloat("Speed", c.rb.velocity.magnitude);
        if(potions.startedAttack)animator.SetTrigger("Attacked");
    }
}
