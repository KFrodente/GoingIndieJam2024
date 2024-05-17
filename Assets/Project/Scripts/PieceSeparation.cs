using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class PieceSeparation : MonoBehaviour
{
    [Header("Pieces")]
    [SerializeField] public Segment head;
    [SerializeField] private Segment tail;
    [SerializeField] private Segment bodyPrefab;

    [Header("Details")]
    [SerializeField] private int length;
    [SerializeField] private float followDistance;
    [SerializeField] private float vibrationRate;
    [SerializeField] private float vibrationAmplitude;
    [SerializeField] private float vibrationOffset;
    [SerializeField] private float scaleDownPercent = 0.9f;

    [Header("other")]
    [SerializeField] private Damagable DragonDamager;
    [SerializeField] private Transform startingLocation;

    private List<Segment> segments = new List<Segment>();

    private float totalStartHealth = 0;
    private void Start()
    {
        
    }

    public float GetHealthPercent()
    {
        Debug.Log(GetRemainingHealth() / totalStartHealth);
        return GetRemainingHealth() / totalStartHealth;
    }

    private float GetRemainingHealth()
    {
        float totalHealth = 0;
        for(int i = 0; i < segments.Count; i++)
        {
            totalHealth += segments[i].getHealth();
        }
        return totalHealth;
    }
    private List<Segment> bodyPieces = new List<Segment>();

    [Button]
    public void SpawnDragon()
    {
        flyPatterns.head = CreateSegment(head, 0);
        for (int i = 0; i < length - 1; i++)
        {
            bodyPieces.Add(CreateSegment(bodyPrefab, i + 1));
        }
        CreateSegment(tail, length + 1);
        totalStartHealth = GetRemainingHealth();
    }

    private Segment CreateSegment(Segment type, int position)
    {
        Segment newSegment = Instantiate(type, startingLocation.position + transform.right * (followDistance * position), Quaternion.identity, transform).GetComponent<Segment>();
        newSegment.Initialize(vibrationAmplitude, vibrationRate, vibrationOffset * position, segments.Count > 0 ? segments[segments.Count - 1] : null, followDistance  * (Mathf.Pow(scaleDownPercent, position)), this);
        segments.Add(newSegment);
        newSegment.transform.localScale = newSegment.transform.localScale * Mathf.Pow(scaleDownPercent, position);
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
        DragonDamager.TakeDamage(brokenSegment.getHealth(), ProjectileDamageType.Acid);
        Debug.Log("Down To: " + segments.Count);
        if (segments.Count <= 1)
        {
            DragonDamager.Die();
        }
    }

    [SerializeField] private DragonFlyPatterns flyPatterns;

}