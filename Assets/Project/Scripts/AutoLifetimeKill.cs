using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLifetimeKill : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject toDestroy;
    private void Start()
    {
        Destroy(toDestroy, lifetime);
    }
}
