using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Possessable : MonoBehaviour
{
    [SerializeField] protected UnityEvent<Soul> OnStartPossess;
    [SerializeField] protected UnityEvent<Transform> OnEndPossess;
    protected Soul possessingSoul;

    public void Possess(Soul t)
    {
        OnStartPossess?.Invoke(t);
        possessingSoul = t;
    }
    
    public void UnPossess(Transform t)
    {
        OnEndPossess?.Invoke(t);
        possessingSoul?.Reanimate(transform);
    }
    
}
