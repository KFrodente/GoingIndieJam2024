using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBar : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;


	void Update()
    {
        if(target)
        {
            transform.rotation = Camera.main.transform.rotation;
            transform.position = target.position + offset;

        }
    }
}
