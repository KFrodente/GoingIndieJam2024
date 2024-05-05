using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnClickDown = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnClickUp = new UnityEvent<Vector2>();

    private void Update()
    {
        OnMove?.Invoke(GetMoveDirection());

        if (Input.GetMouseButtonDown(0))
        {
            OnClickDown?.Invoke(GetMousePosition());
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnClickUp?.Invoke(GetMousePosition());
        }
    }

    protected abstract Vector2 GetMoveDirection();

    private Vector2 GetMousePosition()
    {
        return GetCamera().ScreenToWorldPoint(Input.mousePosition);
    }

    private Camera cam;
    private Camera GetCamera()
    {
        if (cam) return cam;
        cam = Camera.main;
        return cam;
    }
}
