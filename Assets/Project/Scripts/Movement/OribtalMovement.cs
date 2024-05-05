using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class OribtalMovement : CharacterMovement
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float speed;
    public override void Move(Vector2 direction)
    {
        
    }

    public override void Click(Vector2 position)
    {
        turnSpeed *= -1;
    }

    private void Update()
    {
        transform.position += transform.up * (speed * Time.deltaTime);

        transform.RotateAround(transform.position, Vector3.forward, turnSpeed * Time.deltaTime);
    }
}
