using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

public class DragonFlyPatterns : MonoBehaviour
{
    [System.Serializable]
    public struct Path
    {
        public SplineContainer path;
        [Min(0.0001f)] public float duration;

        public Vector2 GetPathPoint(float t)
        {
            float3 pos = path.Spline.EvaluatePosition(t);
            return new Vector2(pos.x, pos.y);
        }
    
        // public Vector2 GetDirectionRight()
        // {
        //     return (path.Ge.transform.position.x > point2.transform.position.x) ? (point1.position - point2.position).normalized : (point2.position - point1.position).normalized;
        // }
        //
        // public float GetNeededSpeed()
        // {
        //     return Vector2.Distance(point1.position, point2.position) / duration;
        // }
        //
        // public Vector2 GetLeftPos()
        // {
        //     return (point1.transform.position.x > point2.transform.position.x) ? point2.position : point1.position;
        // }
    }
    
    [SerializeField] private List<Path> locations = new List<Path>();
    [SerializeField] private float pickRate;

    [HideInInspector] public Segment head;

    private float timer = 0;
    private bool goingLeft;
    private void Update()
    {
        if(timer <= 0) PickPath();
        timer -= Time.deltaTime;

        if (timer - pickRate < 0) return;
        Debug.Log("Time: " + ((timer - pickRate) / selectedPath.duration));
        if(goingLeft)
        {
            head.transform.position = selectedPath.GetPathPoint((timer - pickRate) / selectedPath.duration) + (Vector2)transform.position;
        }
        else
        {
            head.transform.position = selectedPath.GetPathPoint(1 - (timer - pickRate) / selectedPath.duration) + (Vector2)transform.position;
        }
    }

    private void PickPath()
    {
        Path l = GetPath();
        LockOntoPath(l);
        goingLeft = !goingLeft;
        head.GetComponentInChildren<SpriteRenderer>().flipX = goingLeft;
        timer = pickRate + l.duration;
    }

    private Path selectedPath;
    private void LockOntoPath(Path path)
    {
        selectedPath = path;
    }

    private Path GetPath()
    {
        return locations[Random.Range(0, locations.Count)];
    }
}
