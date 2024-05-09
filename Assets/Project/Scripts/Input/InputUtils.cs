using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputUtils
{
    public static Vector2 GetMousePosition()
    {
        return GetCamera().ScreenToWorldPoint(Input.mousePosition);
    }
    private static Camera cam;
    private static Camera GetCamera()
    {
        if (cam) return cam;
        cam = Camera.main;
        return cam;
    }
    public static float GetAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }
}

public struct MouseInputData
{
    public bool leftDown;
    public bool leftUp;
    public bool rightDown;
    public bool rightUp;
    public bool middleDown;
    public bool middleUp;
}