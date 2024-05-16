using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetheredMovement : BaseMovement
{
    public float maxRadius;
    public float currentRadius;

    public float totalRequiredAngle;
    public float currentAngle;

    private void Start()
    {
        currentRadius = maxRadius;
    }

}
