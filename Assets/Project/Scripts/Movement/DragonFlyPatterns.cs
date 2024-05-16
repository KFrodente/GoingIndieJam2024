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
    }

    private void Start()
    {
        timer = incomingPath.duration;
        LockOntoPath(incomingPath);
    }

    [SerializeField] private List<Path> locations = new List<Path>();
    [SerializeField] private Path incomingPath;
    [SerializeField] private float pickRate;

    [HideInInspector] public Segment head;

    private float timer = 0;
    private bool goingLeft;
    private Vector2 lastHeadPos;
    private void Update()
    {
        if(head == null) return;
        if(timer <= 0) PickPath();
        timer -= Time.deltaTime;
            //Quaternion.LookRotation(Vector3.forward, selectedPath.path.EvaluateUpVector((timer - pickRate) / selectedPath.duration));
            //Quaternion.Euler(0, 0, InputUtils.GetAngle(((Vector2)head.transform.position - lastHeadPos)) + 90);
        if (timer - pickRate < 0) return;
        if(goingLeft)
        {
            head.transform.position = selectedPath.GetPathPoint((timer - pickRate) / selectedPath.duration) + (Vector2)transform.position;
        }
        else
        {
            head.transform.position = selectedPath.GetPathPoint(1 - (timer - pickRate) / selectedPath.duration) + (Vector2)transform.position;
        }

        head.transform.rotation = Quaternion.Euler(0, 0, InputUtils.GetAngle(((Vector2)head.transform.position - lastHeadPos)) - 90);
        lastHeadPos = head.transform.position;
    }

    private void PickPath()
    {
        Path l = GetPath();
        LockOntoPath(l);
        goingLeft = !goingLeft;
        head.GetComponentInChildren<SpriteRenderer>().flipY = goingLeft;
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
