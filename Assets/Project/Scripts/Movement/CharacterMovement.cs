using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    public abstract void Move(Vector2 direction);

    public virtual void LeftClickDown(Vector2 position)
    {
    }

    public virtual void RightClickDown(Vector2 position)
    {
    }

}
