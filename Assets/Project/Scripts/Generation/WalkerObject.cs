using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject
{
    public Vector2 position;
    public Vector2 direction;

    public WalkerObject(Vector2 pos, Vector2 dir)
    {
        position = pos;
        direction = dir;
    }
}
