using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Possessable : MonoBehaviour
{
    [SerializeField] protected UnityEvent<Transform> OnStartPossess;
    [SerializeField] protected UnityEvent<Transform> OnEndPossess;

    public void Possess(Transform t)
    {
        OnStartPossess?.Invoke(t);
        Debug.Log("ACTUALLY POSSESSED ");
    }
    
    public void UnPossess(Transform t)
    {
        OnEndPossess?.Invoke(t);
    }
    
}
