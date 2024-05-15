using System;
using NaughtyAttributes;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flowPart;
    private float range;
    private float speed;
    private Segment pieceFollowing;
    private float distance;

    private bool left;
    private float startTime;

    private PieceSeparation main;
    public void Initialize(float range, float speed, float offset, Segment pieceFollowing, float distance, PieceSeparation main)
    {
        startTime = offset + Time.time;
        this.main = main;
        this.range = range;
        this.speed = speed;
        this.pieceFollowing = pieceFollowing;
        this.distance = distance;
    }

    public void SetFollowingPiece(Segment piece)
    {
        pieceFollowing = piece;
    }

    [Button]
    public void LoseSegment()
    {
        main.Lose(this);
    }
    private void Update()
    {
        Follow();
        if(startTime < Time.time) MoveGraphic();

    }
    
    
    

    private void Follow()
    {
        if (pieceFollowing == null) return;
        if (Vector2.Distance(pieceFollowing.transform.position, transform.position) > distance)
        {
            Vector2 direction = (transform.position - pieceFollowing.transform.position).normalized;
            transform.position = (Vector2)pieceFollowing.transform.position + (direction * distance);
            transform.rotation = Quaternion.Euler(0, 0, InputUtils.GetAngle(direction) + 90);
        }
    }

    
    private void MoveGraphic()
    {
        if (left)
        {
            flowPart.transform.position += transform.right * (Time.deltaTime * speed);
        }
        else
        {
            flowPart.transform.position -= transform.right * (Time.deltaTime * speed);
        }
        if(Vector2.Distance(transform.position, flowPart.transform.position) > range)
        {
            flowPart.transform.position = transform.position + ((left) ? transform.right * range : -transform.right * range);
            left = !left;
        }
    }
}