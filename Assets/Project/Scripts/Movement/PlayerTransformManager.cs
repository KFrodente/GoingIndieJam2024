using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformManager : MonoBehaviour
{
    public static PlayerTransformManager instance;
    private void Awake()
    {
        instance = this;
    }
    public Transform playerTransform;

    private void OnDestroy()
    {
        instance = null;
    }
}
