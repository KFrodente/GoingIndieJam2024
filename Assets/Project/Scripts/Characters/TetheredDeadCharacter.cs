using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class TetheredDeadCharacter : BaseCharacter
{
    [SerializeField] private GameObject tetheredBody;
    [SerializeField] private GameObject spirit;
    [SerializeField] private LineRenderer lr;

    public GameObject connectedObject;

    private Vector3 direction;
    private Vector3 prevDirection = Vector3.up;

    private float prevTotalAngle = 1;
    private float prevprevTotalAngle = 0;
    //private Vector3 prevprevDirection = Vector3.up;
    // private float distance;
    // private float theta;

    private float addedAngle;

    public float maxRadius;
    public float currentRadius;

    public float totalRequiredAngle;
    public float currentAngle;
    
    private void Start()
    {
        currentRadius = maxRadius;
    }

    protected override void Update()
    {
        base.Update();

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector2 offset = (connectedObject.transform.position) - (transform.position);
        float distance = offset.magnitude;



        direction = offset / distance;

        addedAngle = Mathf.Sin(InputUtils.GetAngle(direction) - InputUtils.GetAngle(prevDirection));
        //float prevAddedAngle = Mathf.Sin(InputUtils.GetAngle(prevDirection) - InputUtils.GetAngle(prevprevDirection));

        float percentComplete = Mathf.Abs(currentAngle) / totalRequiredAngle;

        float whatWillBeAdded = Mathf.Abs(addedAngle * (1 + (Vector2.Distance(connectedObject.transform.position, transform.position) / currentRadius)));
        if (percentComplete <= .6f)
        {
            if (addedAngle > 0) currentAngle += whatWillBeAdded;
            else currentAngle -= whatWillBeAdded;
        }
        else
        {
            currentAngle += Mathf.Sign(currentAngle) * whatWillBeAdded * 4;//* Mathf.Abs(addedAngle) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 7;
        }

        currentRadius = maxRadius * (1 - (Mathf.Abs(currentAngle) / totalRequiredAngle));

        if (distance > currentRadius)
        {
            connectedObject.transform.position = transform.position + direction * currentRadius;
        }

        if (percentComplete >= .9f)
        {
            if (connectedObject != spirit)
            {
                connectedObject.GetComponent<Damagable>().Die();
            }
            currentAngle = 0;
            currentRadius = maxRadius;
            tetheredBody.transform.position = transform.position;
            tetheredBody.SetActive(true);
            gameObject.SetActive(false);
            tetheredBody.GetComponent<Damagable>().RefillHealth();
            tetheredBody.GetComponentInChildren<Possessable>().OnInteract(spirit.GetComponent<BaseCharacter>());
        }

        Vector3 chainBodyPos = transform.position;
        chainBodyPos.y -= 0.4f;
        lr.SetPosition(0, chainBodyPos);
        lr.SetPosition(1, connectedObject.transform.position);
        //lr.SetPosition(1, tetheredDeadBody.transform.position);

        prevDirection = direction;
    }

    public void SetConnectedObject(GameObject connectedObject)
    {
        this.connectedObject = connectedObject;
    }
}
