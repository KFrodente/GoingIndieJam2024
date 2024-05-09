using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRoom : Room
{
    Vector2 topP;
    Vector2 bottomP;
    Vector2 leftP;
    Vector2 rightP;

    public void makePortals(Vector2 top, Vector2 right, Vector2 bottom, Vector2 left)
    {
        GeneratePortals();
    }

    public override void GeneratePortals()
    {
        
    }
}
