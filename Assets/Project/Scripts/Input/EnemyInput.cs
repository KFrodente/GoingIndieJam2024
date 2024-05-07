using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    private Transform playerCharacter;
    protected override Vector2 GetMoveDirection()
    {
        return (playerCharacter.position - transform.position).normalized;
    }
    
    protected void Update()
    {
        base.Update();
        if ((playerCharacter.position - transform.position).magnitude > 0)
        {
            
        }
    }
}
