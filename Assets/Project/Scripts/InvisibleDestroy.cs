using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class InvisibleDestroy : MonoBehaviour
{
    [SerializeField] private GameObject destroyed;
    private void OnBecameInvisible()
    {
        Destroy(destroyed);
    }
}
