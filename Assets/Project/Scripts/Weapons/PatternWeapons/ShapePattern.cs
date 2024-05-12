using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape_Pattern", menuName = "Patterns/Shape")]
public class ShapePattern : Pattern
{
	[Header("Shape")]
	[SerializeField] Vector2[] points;

	[SerializeField] bool evenAmountBetweenPoints;
	[SerializeField] bool addEndcaps;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		float totaldistance = 0;
		for (int i = 0; i < points.Length; i++)
		{
			if (i - 1 >= 0)
			{
				totaldistance += Vector2.Distance(points[i - 1], points[i]);
			}
		}

		int length = bulletAmount;
		float neededDistance = totaldistance / bulletAmount;

		if (evenAmountBetweenPoints)
		{
			length = bulletAmount * (points.Length - 1);
			neededDistance = totaldistance / points.Length;
		}
		else if (addEndcaps)
		{
			if (bulletAmount < points.Length - 1)
			{
				bulletAmount = points.Length - 1;
			}
			length = bulletAmount + (points.Length) - 1;
			//Debug.Log(length + " ____ " + bulletAmount);
		} 

		Vector3[] positions = new Vector3[length];

		float distanceProgress = neededDistance/2.0f;
		int currentpoint = 0;

		if (evenAmountBetweenPoints)
		{
			for (int i = 1; i < points.Length; i++)
			{
				//float currentlinedistance = Vector3.Distance(points[currentpoint - 1], points[currentpoint]);

				for (int o = 0; o < bulletAmount; o++)
				{
					Debug.Log("On POint " + i + ";  spawning bullet " + o + ";  position is " + (o + (bulletAmount * (i - 1))) + "/" + positions.Length);
					positions[o + (bulletAmount * (i - 1))] = Vector3.Lerp(points[i - 1], points[i], (float)o/bulletAmount);
				}
			}
		}
		else
		{
			for (int i = 0; i < positions.Length; i++)
			{
				if (currentpoint < points.Length - 1)
				{
					float currentlinedistance = Vector3.Distance(points[currentpoint], points[currentpoint + 1]);

					positions[i] = Vector3.Lerp(points[currentpoint], points[currentpoint + 1], distanceProgress/ currentlinedistance) * scalar;
					positions[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positions[i];
					distanceProgress += neededDistance;

					if (distanceProgress >= currentlinedistance - 0.05f)
					{
						//Debug.Log(positions.Length + " ____ " + i);
						if (addEndcaps)
						{
							i++;
							positions[i] = points[currentpoint + 1] * scalar;
							positions[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positions[i];
						}
						distanceProgress -= currentlinedistance;
						currentpoint++;
					}
				}
			}
		}

		return positions;
	}
}
