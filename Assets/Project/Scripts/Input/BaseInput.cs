using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMoveUpdate = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveFixed = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnLeftClickDown = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnLeftClickUp = new UnityEvent<Vector2>();
	public UnityEvent<Vector2> OnRightClickDown = new UnityEvent<Vector2>();
	public UnityEvent<Vector2> OnRightClickUp = new UnityEvent<Vector2>();

	protected void Update()
    {
        OnMoveUpdate?.Invoke(GetMoveDirection().normalized);

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

	protected void FixedUpdate()
	{
		OnMoveFixed?.Invoke(GetMoveDirection());
	}

	protected virtual Vector2 GetMoveDirection()
	{
		return Vector2.zero;
		
	}

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
