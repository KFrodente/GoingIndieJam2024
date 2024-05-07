using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PossessInput : PlayerInput
{
    [SerializeField] private KeyCode key;
    [SerializeField] private UnityEvent OnTryPossess;
    protected void Update()
    {
        base.Update();
        if (Input.GetKeyDown(key))
        {
            OnTryPossess?.Invoke();
        }
    }
}
