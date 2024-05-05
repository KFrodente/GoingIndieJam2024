using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerObject
{
    public Vector2 position;
    public Vector2 direction;
    public float chanceToRemove;
    public float chanceToRedirect;
    public int failedRedirects;
    public float chanceToCreate;
    public List<Vector2> prevDirections = new();

    public int walkerNum;

    public WalkerObject(Vector2 pos, Vector2 dir, float chanceToRemove, float chanceToRedirect, float chanceToCreate, int walkerNum)
    {
        position = pos;
        direction = dir;
        this.chanceToRedirect = chanceToRedirect;
        this.chanceToRemove = chanceToRemove;
        this.chanceToCreate = chanceToCreate;
        this.walkerNum = walkerNum;
    }
}
