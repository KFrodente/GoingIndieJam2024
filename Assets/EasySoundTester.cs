using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasySoundTester : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();


    private int p = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(objects[p]);
            p++;
            if (p >= objects.Count) p = 0;
        }
    }
}
