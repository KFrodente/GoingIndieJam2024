using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Circle_Pattern", menuName = "Patterns/Circle")]
public class CirclePattern : Pattern
{
	[Header("Circle")]
	[SerializeField] float circleRadius;
	[SerializeField] float arcSpawnedOn = 360;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector3 spawnPlacement)
	{
		Vector3[] positions = new Vector3[bulletAmount];

		for (int i = 0; i < bulletAmount; i++)
		{
			float spawnAngle = (arcSpawnedOn / bulletAmount) * i;
			positions[i] = (spawnPlacement + (Quaternion.Euler(0, 0, spawnAngle) * direction * circleRadius));
			//positions.Add(Instantiate((bulletPrefab == null) ? prefab : bulletPrefab, spawnPlacement + position, Quaternion.Euler(0, 0, spawnAngle)));
		}

		return positions;
	}
}
