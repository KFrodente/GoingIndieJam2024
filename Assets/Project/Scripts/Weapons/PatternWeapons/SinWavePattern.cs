using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sin_Wave_Pattern", menuName = "Patterns/Sin Wave")]
public class SinWavePattern : Pattern
{
	[Header("SinWave")]
	[SerializeField] float amplitude = 1;
	[SerializeField] float frequency = 1f;
	[SerializeField] float xIntervals = 0.1f;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		Vector3[] positions = new Vector3[bulletAmount];

		float fakeTime = 0;
		for (int i = 0; i < positions.Length; i++)
		{
			positions[i].y = Mathf.Sin(fakeTime * frequency) * amplitude;
			positions[i].x = fakeTime;
			fakeTime += xIntervals;

			positions[i] = positions[i] * scalar;
			positions[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positions[i];
		}

		return positions;
	}
}
