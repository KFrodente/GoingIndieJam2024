using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSeparation : MonoBehaviour
{
    [SerializeField] private Segment head;
    [SerializeField] private Segment tail;
    [SerializeField] private List<Segment> segments = new List<Segment>();
    //[SerializeField] private float separation;

    private void Update()
    {
        //SetSegmentSeparation();
    }

    public void SetSegmentSeparation()
    {
        // for (int i = 0; i < segments.Count - 1; i++)
        // {
        //     segments[i].distance = separation;
        //     if(i == 0) segments[i].pieceFollowing = head;
        //     else segments[i].pieceFollowing = segments[i - 1].pieceFollowing;
        // }
        //
        // if (segments.Count > 1)
        // {
        //     tail.pieceFollowing = segments[segments.Count - 1];
        //     tail.distance = separation;
        // }
    }
}