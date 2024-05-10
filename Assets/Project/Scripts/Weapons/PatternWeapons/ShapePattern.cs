using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "Shape_Pattern", menuName = "Patterns/Shape")]
public class ShapePattern : Pattern
{
	[Header("Shape")]
	[SerializeField] Vector2[] points;
	[SerializeField] Vector2 scalar;

	public override Vector3[] SpawnBullets(Vector3 direction)
	{
		Vector3[] positions = new Vector3[bulletAmount];

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

				positions[i] = Vector3.Lerp(points[currentpoint], points[currentpoint + 1], distanceProgress/ currentlinedistance);
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
