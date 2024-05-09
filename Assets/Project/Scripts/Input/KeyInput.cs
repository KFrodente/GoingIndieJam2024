using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyInput : MonoBehaviour
{
	[SerializeField] private KeyCode key;
	[SerializeField] private UnityEvent OnKeyPressed;
	[SerializeField] private UnityEvent OnKeyReleased;

	private void Update()
	{
		if(Input.GetKeyDown(key)) OnKeyPressed?.Invoke();
		if(Input.GetKeyUp(key)) OnKeyReleased?.Invoke();
	}
}
