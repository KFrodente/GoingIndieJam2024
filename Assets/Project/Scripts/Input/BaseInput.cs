using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    public virtual Vector2 GetNormalizedMoveDirection()
    {
        return Vector2.zero;
    }
    public virtual MouseInputData GetMouseInput()
    {
        return new MouseInputData();
    }

    public virtual bool GetInputType()
    {
        return false;
    }

    public virtual Transform GetInputTarget()
    {
        return null;
        
    }

    protected virtual void Update()
    {
        
    }
}
