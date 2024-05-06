using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnLeftClickDown = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnLeftClickUp = new UnityEvent<Vector2>();
	public UnityEvent<Vector2> OnRightClickDown = new UnityEvent<Vector2>();
	public UnityEvent<Vector2> OnRightClickUp = new UnityEvent<Vector2>();

	private void Update()
    {
        OnMove?.Invoke(GetMoveDirection());

        if (Input.GetMouseButtonDown(0))
        {
            OnLeftClickDown?.Invoke(GetMousePosition());
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnLeftClickUp?.Invoke(GetMousePosition());
        }

		if (Input.GetMouseButtonDown(1))
		{
			OnRightClickDown?.Invoke(GetMousePosition());
		}
		if (Input.GetMouseButtonUp(1))
		{
			OnRightClickUp?.Invoke(GetMousePosition());
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
