using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TetheredSpiritCharacter : SpiritCharacter
{
    [SerializeField] private GameObject tetheredDeadBody;
    [SerializeField] private GameObject tetheredBody;
    [SerializeField] private LineRenderer lr;


    private Vector3 direction;
    private Vector3 prevDirection = Vector3.up;

    private float prevTotalAngle = 1;
    private float prevprevTotalAngle = 0;
    //private Vector3 prevprevDirection = Vector3.up;
   // private float distance;
   // private float theta;

    private TetheredMovement tetheredMovement;
    private float addedAngle;

    private void Start()
    {
        //currentRadius = maxRadius;
        tetheredMovement = movement.GetComponent<TetheredMovement>();
    }

    protected override void Update()
    {
        base.Update();
        movement.SetTargetAngle((rb.velocity));
        movement.AngleTowardTargetAngle(999f, this);

        Vector2 offset = (transform.position * 5) - (tetheredDeadBody.transform.position * 5);
        float distance = offset.magnitude;



        direction = offset / distance;

        addedAngle = Mathf.Sin(InputUtils.GetAngle(direction) - InputUtils.GetAngle(prevDirection));
        //float prevAddedAngle = Mathf.Sin(InputUtils.GetAngle(prevDirection) - InputUtils.GetAngle(prevprevDirection));

        float percentComplete = Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle;

        float whatWillBeAdded = Mathf.Abs(addedAngle * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)));
        if (percentComplete <= .6f)
        {
            if (addedAngle > 0) tetheredMovement.currentAngle += whatWillBeAdded;
            else tetheredMovement.currentAngle -= whatWillBeAdded;
        }
        else
        {
            tetheredMovement.currentAngle += Mathf.Sign(tetheredMovement.currentAngle) * whatWillBeAdded * 4;//* Mathf.Abs(addedAngle) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 7;
        }

        tetheredMovement.currentRadius = tetheredMovement.maxRadius * (1 - (Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle));

        if ((transform.position - tetheredDeadBody.transform.position).magnitude > tetheredMovement.currentRadius)
        {
            transform.position = tetheredDeadBody.transform.position + direction * tetheredMovement.currentRadius;
        }

        if (percentComplete >= .9f)
        {
            tetheredMovement.currentAngle = 0;
            tetheredMovement.currentRadius = tetheredMovement.maxRadius;
            tetheredBody.transform.position = tetheredDeadBody.transform.position;
            tetheredBody.SetActive(true);
            tetheredDeadBody.SetActive(false);
            tetheredBody.GetComponentInChildren<Possessable>().OnInteract(this);
            tetheredBody.GetComponent<Damagable>().RefillHealth();
        }
        Vector3 chainBodyPos = tetheredDeadBody.transform.position;
        chainBodyPos.y -= 0.4f;
        lr.SetPosition(1, chainBodyPos);
        lr.SetPosition(0, transform.position);
        //lr.SetPosition(1, tetheredDeadBody.transform.position);

        prevDirection = direction;
    }


    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();

    //    Vector2 offset = (transform.position * 3) - (tetheredDeadBody.transform.position * 3);
    //    float distance = offset.magnitude;



    //    direction = offset / distance ;

    //    addedAngle = Mathf.Sin(InputUtils.GetAngle(direction) - InputUtils.GetAngle(prevDirection));

    //    float percentComplete = Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle;

    //    if (percentComplete <= .5f)
    //    {
    //        if (addedAngle > 0) tetheredMovement.currentAngle += Mathf.Abs(addedAngle * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 2f);
    //        else tetheredMovement.currentAngle -= Mathf.Abs(addedAngle * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 2f);
    //    }
    //    else
    //    {
    //        tetheredMovement.currentAngle += Mathf.Sign(tetheredMovement.currentAngle) * Mathf.Abs(addedAngle) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 5;
    //    }

    //    tetheredMovement.currentRadius = tetheredMovement.maxRadius * (1 - (Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle));

    //    if ((transform.position - tetheredDeadBody.transform.position).magnitude > tetheredMovement.currentRadius)
    //    {
    //        transform.position = tetheredDeadBody.transform.position + direction * tetheredMovement.currentRadius;
    //    }

    //    if (percentComplete >= .9f)
    //    {
    //        tetheredMovement.currentAngle = 0;
    //        tetheredMovement.currentRadius = tetheredMovement.maxRadius;
    //        tetheredBody.SetActive(true);
    //        tetheredDeadBody.SetActive(false);
    //        tetheredBody.GetComponentInChildren<Possessable>().OnInteract(this);
    //    }

    //    prevDirection = direction;
    //}

    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    Vector2 offset = transform.position - tetheredDeadBody.transform.position;
    //    float distance = offset.magnitude;



    //    direction = offset / distance;
    //    Vector2 a = new Vector2(tetheredDeadBody.transform.position.x - transform.position.x, tetheredDeadBody.transform.position.y - transform.position.y);
    //    Vector2 b = new Vector2(tetheredDeadBody.transform.position.x - prevDirection.x, tetheredDeadBody.transform.position.y - prevDirection.y);

    //    addedAngle = Mathf.Sin(InputUtils.GetAngle(direction) - InputUtils.GetAngle(prevDirection));

    //    float theta = Mathf.Acos(Vector2.Dot(a.normalized, b.normalized));
    //    if (addedAngle < 0) tetheredMovement.currentAngle -= Mathf.Abs(theta - prevTheta) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius));
    //    if (addedAngle > 0) tetheredMovement.currentAngle += Mathf.Abs(theta - prevTheta) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius));


    //    tetheredMovement.currentRadius = tetheredMovement.maxRadius * (1 - (Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle));

    //    if (distance > tetheredMovement.currentRadius)
    //    {
    //        transform.position = tetheredDeadBody.transform.position + direction * tetheredMovement.currentRadius;
    //    }

    //    prevDirection = direction;
    //    prevTheta = theta;
    //}


}
