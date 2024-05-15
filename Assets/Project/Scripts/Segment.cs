using System;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private Transform flowPart;
    [SerializeField, Min(0)] private float range;
    [SerializeField, Min(0)] private float speed;

    public Segment pieceFollowing;
    public float distance;

    private bool left;
    private void Update()
    {
        Follow();
        MoveGraphic();
        
    }

    private void Follow()
    {
        if (pieceFollowing == null) return;
        if (Vector2.Distance(pieceFollowing.transform.position, transform.position) > distance)
        {
            Vector2 direction = (pieceFollowing.transform.position - transform.position).normalized;
            transform.position = (Vector2)pieceFollowing.transform.position + (direction * distance);
        }
    }

    private void MoveGraphic()
    {
        if (left)
        {
            flowPart.position += transform.right * (Time.deltaTime * speed);
        }
        else
        {
            flowPart.position -= transform.right * (Time.deltaTime * speed);
        }
        if(Vector2.Distance(transform.position, flowPart.position) > range)
        {
            flowPart.position = transform.position + ((left) ? transform.right * range : -transform.right * range);
            left = !left;
        }
    }
}