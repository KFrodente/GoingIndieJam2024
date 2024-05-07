using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : BaseInput
{
    
    
    protected void Update()
    {
        Vector3 position = PlayerTransformManager.instance.playerTransform.position;
        
        OnMoveUpdate?.Invoke(position);
        OnLeftClickDown?.Invoke(position);
    }

    protected void FixedUpdate()
    {
        base.FixedUpdate();
        OnMoveFixed?.Invoke(PlayerTransformManager.instance.playerTransform.position);
    }
}
