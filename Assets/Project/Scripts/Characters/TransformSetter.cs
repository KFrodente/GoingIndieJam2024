using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSetter : MonoBehaviour
{
    [SerializeField] private bool starterCharacter;

    private void Start()
    {
        if (!starterCharacter) return;
        SetAsMainTransform(this.transform);
    }

    public void SetAsMainTransform(Transform t)
    {
        //PlayerTransformManager.instance.playerTransform = t;
    }
    
}
