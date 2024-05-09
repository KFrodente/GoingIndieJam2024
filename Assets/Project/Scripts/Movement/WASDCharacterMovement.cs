using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDCharacterMovement : CharacterMovement
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected StatHandler statHandler;
    // public override void Move(Vector2 direction)
    // {
    //     Move(direction, ForceMode2D.Force);
    // }
    //
    // public override void Move(Vector2 direction, ForceMode2D forceMode, bool force = false)
    // {
    //     if (!canMove && !force) return;
    //     if (direction == Vector2.zero) return;
    //     rb.AddForce(direction * statHandler.stats.MoveSpeed, forceMode);
    //     SetTargetAngle(direction);
    // }
    //
    // public override void SetTargetAngle(Vector2 direction)
    // {
    //     targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    // }
    //
    // private void Update()
    // {
    //     AngleTowardTargetAngle();
    // }
    //
    // private float targetAngle;
    // public override void AngleTowardTargetAngle()
    // {
    //     Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
    //     transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, statHandler.stats.TurnSpeed * Time.deltaTime);
    // }
}
