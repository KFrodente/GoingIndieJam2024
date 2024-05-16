using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    [SerializeField] private Vector2 endScale;
    [SerializeField] private float time;
    [SerializeField] private Transform ps ;
    private float timer;
    private void Start()
    {
        timer = time;
        startScale = transform.localScale;
        
    }

    private Vector2 startScale;
    private void Update()
    {
        timer -= Time.deltaTime;
        transform.localScale = Vector3.Lerp(endScale, startScale, timer / time);
        ps.localScale = transform.localScale;
    }
}
