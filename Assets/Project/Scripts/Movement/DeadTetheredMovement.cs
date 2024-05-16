using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadTetheredMovement : EnemyMovement
{
    public override void Move(Vector2 direction, float speed, ForceMode2D forceMode, BaseCharacter c, bool forcedAction = false)
    {

    }

    public override void ExplodeAway(Vector2 center, float power)
    {
        //base.ExplodeAway(center, power);
        savedCharacter.rb.AddForce(((Vector2)transform.position - center).normalized * power, ForceMode2D.Impulse);
    }
}
