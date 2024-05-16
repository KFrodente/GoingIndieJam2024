using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAnimationHandler : MonoBehaviour
{
	[SerializeField] Animator animator;
	[SerializeField] BaseCharacter c;
	private void Update()
	{
		animator.SetFloat("Speed", c.rb.velocity.magnitude);
	}
}
