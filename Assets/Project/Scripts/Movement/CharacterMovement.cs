using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    public abstract void Move(Vector2 direction);

    public abstract void LeftClick(Vector2 position);

    public abstract void RightClick(Vector2 position);
    
}
