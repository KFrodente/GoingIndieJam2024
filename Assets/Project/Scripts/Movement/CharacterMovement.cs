using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    protected bool canMove = true;
    public abstract void Move(Vector2 direction);

    public virtual void Move(Vector2 direction, ForceMode2D forceMode, bool force = false)
    {
    }

    public virtual void LeftClickDown(Vector2 position)
    {
    }

    public virtual void RightClickDown(Vector2 position)
    {
    }
    public virtual void SetTargetAngle(Vector2 direction)
    {
    }
    public virtual void AngleTowardTargetAngle()
    {
    }
    public void SetMovement(bool canMove)
    {
        this.canMove = canMove;
    }

}
