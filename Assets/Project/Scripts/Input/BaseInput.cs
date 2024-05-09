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

    protected virtual void Update()
    {
        
    }
}
