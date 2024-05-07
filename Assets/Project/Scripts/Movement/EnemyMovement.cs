using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : CharacterMovement
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    public override void Move(Vector2 direction)
    {
        rb.AddForce(speed * direction.normalized);
    }
    
}
