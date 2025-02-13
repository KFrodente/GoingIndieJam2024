using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInput : MonoBehaviour
{
    public virtual Vector2 GetNormalizedMoveDirection()
    {
        return Vector2.zero;
    }
    public virtual MouseInputData GetMouseInput()
    {
        return new MouseInputData();
    }
    

    public virtual Target GetInputTarget()
    {
        return null;
        
    }

}
