using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    public abstract void Move(Vector2 direction);


    public abstract void Click(Vector2 position);
    
}
