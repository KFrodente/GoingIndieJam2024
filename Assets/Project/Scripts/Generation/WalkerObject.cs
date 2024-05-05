using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject
{
    public Vector2 position;
    public Vector2 direction;
    public float chanceToRemove;
    public float chanceToRedirect;
    public float chanceToCreate;
    public List<Vector2> prevDirections = new();
    public List<Vector2> placedPieces = new();

    public WalkerObject(Vector2 pos, Vector2 dir, float chanceToRemove, float chanceToRedirect, float chanceToCreate)
    {
        position = pos;
        direction = dir;
        this.chanceToCreate = chanceToCreate;
        this.chanceToRemove = chanceToRemove;
        this.chanceToCreate = chanceToCreate;
    }
}
