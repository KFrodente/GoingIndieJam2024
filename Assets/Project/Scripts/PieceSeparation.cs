using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSeparation : MonoBehaviour
{
    [Header("Pieces")]
    [SerializeField] private Segment head;
    [SerializeField] private Segment tail;
    [SerializeField] private Segment bodyPrefab;

    [Header("Details")]
    [SerializeField] private int length;
    [SerializeField] private float followDistance;
    [SerializeField] private float vibrationRate;
    [SerializeField] private float vibrationAmplitude;
    [SerializeField] private float vibrationOffset;

    [Header("other")]
    [SerializeField] private Damagable DragonDamager;

    private List<Segment> segments = new List<Segment>();
    private void Start()
    {
        flyPatterns.head = CreateSegment(head, 0);
        for (int i = 0; i < length - 1; i++)
        {
            CreateSegment(bodyPrefab, i + 1);
        }
        CreateSegment(tail, length + 1);
    }

    private Segment CreateSegment(Segment type, int position)
    {
        Segment newSegment = Instantiate(type, transform.position + transform.right * followDistance * position, Quaternion.identity, transform).GetComponent<Segment>();
        newSegment.Initialize(vibrationAmplitude, vibrationRate, vibrationOffset * position, segments.Count > 0 ? segments[segments.Count - 1] : null, followDistance, this);
        segments.Add(newSegment);
        return newSegment;
    }

    private void Reposition()
    {
        for(int i = 1; i < segments.Count; i++)
        {
            segments[i].SetFollowingPiece(segments[i - 1]);
        }
    }
    

    public void Lose(Segment brokenSegment)
    {
        segments.Remove(brokenSegment);
        brokenSegment.transform.gameObject.SetActive(false);
        Reposition();
        if (segments.Count <= 2)
        {
            DragonDamager.Die();
        }
    }

    [SerializeField] private DragonFlyPatterns flyPatterns;

}