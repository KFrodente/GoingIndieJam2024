using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RangedEnemyMovement : CharacterMovement
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BaseCharacter character;
    [SerializeField] private Transform playerCharacter;
    public override void Move(Vector2 direction)
    {
        
    }
}
