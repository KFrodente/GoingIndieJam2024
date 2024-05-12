using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Shape_Pattern", menuName = "Patterns/Shape")]
public class ShapePattern : Pattern
{
	[Header("Shape")]
	[SerializeField] Vector2[] points;

	[SerializeField] bool evenAmountBetweenPoints;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		Vector3[] positions;

		if (evenAmountBetweenPoints)
		{
			positions = new Vector3[bulletAmount * (points.Length - 1)];
		} else
		{
			positions = new Vector3[bulletAmount];
		}

		float totaldistance = 0;
		for (int i = 0; i < points.Length; i++)
		{
			if (i - 1 >= 0)
			{
				totaldistance += Vector2.Distance(points[i - 1], points[i]);
			}
		}
		float neededDistance = totaldistance / bulletAmount;
		float distanceProgress = 0;
		int currentpoint = 0;

		for (int i = 0; i < positions.Length; i++)
		{
			if (currentpoint < points.Length - 1)
			{
				float currentlinedistance = Vector3.Distance(points[currentpoint], points[currentpoint + 1]);

				positions[i] = Vector3.Lerp(points[currentpoint], points[currentpoint + 1], distanceProgress/ currentlinedistance) * scalar;
				positions[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positions[i];
				distanceProgress += neededDistance;

				if (distanceProgress >= currentlinedistance)
				{
					distanceProgress -= currentlinedistance;
					currentpoint++;
				}
			}
		}

		return positions;
	}
}
