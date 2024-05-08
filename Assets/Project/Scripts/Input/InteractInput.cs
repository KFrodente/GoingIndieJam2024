using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractInput : MonoBehaviour
{
	[SerializeField] private KeyCode key;
    public UnityEvent OnInteract = new UnityEvent();

	protected void Update()
    {
        
		if (Input.GetKeyDown(key))
		{
			OnInteract?.Invoke();
		}
	}

	
}
