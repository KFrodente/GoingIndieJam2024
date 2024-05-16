using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class TetheredSpiritCharacter : SpiritCharacter
{
    [SerializeField] private GameObject tetheredDeadBody;
    [SerializeField] private GameObject tetheredBody;
    [SerializeField] private LineRenderer lr;


    private Vector3 direction;
    private Vector3 prevDirection = Vector3.up;
    private float prevTheta = 0;
   // private float distance;
   // private float theta;

    private TetheredMovement tetheredMovement;
    private float addedAngle;

    private void Start()
    {
        tetheredMovement = movement.GetComponent<TetheredMovement>();
    }

    protected override void Update()
    {
        base.Update();
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, tetheredDeadBody.transform.position);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Vector2 offset = transform.position - tetheredDeadBody.transform.position;
        float distance = offset.magnitude;



        direction = offset / distance;

        addedAngle = Mathf.Sin(InputUtils.GetAngle(direction) - InputUtils.GetAngle(prevDirection));

        float percentComplete = Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle;

        if (percentComplete <= .5f)
        {
            if (addedAngle > 0) tetheredMovement.currentAngle += Mathf.Abs(addedAngle) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 1.5f;
            else tetheredMovement.currentAngle -= Mathf.Abs(addedAngle) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 1.5f;
        }
        else
        {
            tetheredMovement.currentAngle += Mathf.Sign(tetheredMovement.currentAngle) * Mathf.Abs(addedAngle) * (1 + (Vector2.Distance(transform.position, tetheredDeadBody.transform.position) / tetheredMovement.currentRadius)) * 5;
        }

        tetheredMovement.currentRadius = tetheredMovement.maxRadius * (1 - (Mathf.Abs(tetheredMovement.currentAngle) / tetheredMovement.totalRequiredAngle));

        if (distance > tetheredMovement.currentRadius)
        {
            transform.position = tetheredDeadBody.transform.position + direction * tetheredMovement.currentRadius;
        }

        if (percentComplete >= .9f)
        {
            tetheredMovement.currentAngle = 0;
            tetheredMovement.currentRadius = tetheredMovement.maxRadius;
            tetheredBody.SetActive(true);
            tetheredDeadBody.SetActive(false);
            tetheredBody.GetComponentInChildren<Possessable>().OnInteract(this);
        }

        prevDirection = direction;
    }

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
