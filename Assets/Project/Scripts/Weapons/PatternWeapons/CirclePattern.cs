using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Circle_Pattern", menuName = "Patterns/Circle")]
public class CirclePattern : Pattern
{
	[Header("Circle")]
	[SerializeField] float circleRadius;
	[SerializeField] float arcSpawnedOn = 360;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		Vector3[] positions = new Vector3[bulletAmount];

		for (int i = 0; i < bulletAmount; i++)
		{
			float spawnAngle = (arcSpawnedOn / bulletAmount) * i;
			if (arcSpawnedOn != 360)
			{
				spawnAngle += (arcSpawnedOn/bulletAmount) * 0.5f;
			}
			positions[i] = (Vector2.left * circleRadius);
			positions[i] = Quaternion.Euler(0, 0, spawnAngle - (arcSpawnedOn * 0.5f)) * positions[i] * scalar;
			positions[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positions[i];

			//positions.Add(Instantiate((bulletPrefab == null) ? prefab : bulletPrefab, spawnPlacement + position, Quaternion.Euler(0, 0, spawnAngle)));
		}

		return positions;
	}
}
