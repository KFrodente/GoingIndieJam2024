using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLifetimeKill : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject destroyNotThis;
    private void Start()
    {
        if (destroyNotThis == null) destroyNotThis = this.gameObject;
        Destroy(destroyNotThis, lifetime);
        
    }
}
