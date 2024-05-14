using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sin_Wave_Pattern", menuName = "Patterns/Sin Wave")]
public class SinWavePattern : Pattern
{
	[Header("SinWave")]
	[Tooltip("Height of the wave")]
	[SerializeField] float amplitude = 1;
	[Tooltip("Fequency of waves cycles occuring")]
	[SerializeField] float frequency = 1f;
	[Tooltip("How far along the wave each bullet should spawn")]
	[SerializeField] float SineIntervals = 0.1f;
	[Tooltip("Distance between bullets")]
	[SerializeField, ShowIf("SeperateDistanceFromInterval")] float distance = 0.1f;
	[Tooltip("Initial offset along the sine wave")]
	[SerializeField] float initialOffset = 0;

	[Tooltip("If amplitude should be used in terms of PI")]
	[SerializeField] bool AmplitudeInTermsOfPi;
	[Tooltip("If frequency should be used in terms of PI")]
	[SerializeField] bool FrequencyInTermsOfPi;
	[Tooltip("If the inital offset should be used in terms of PI")]
	[SerializeField] bool OffsetInTermsOfPi;
	[Tooltip("If the Sine Interval should be used in terms of PI")]
	[SerializeField] bool IntervalInTermsOfPi;
	[Tooltip("If Bullets should be placed using distance")]
	[SerializeField] bool SeperateDistanceFromInterval;
	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		Vector3[] positions = new Vector3[bulletAmount];

		float fakeTime = ((OffsetInTermsOfPi) ? initialOffset * Mathf.PI : initialOffset);
		for (int i = 0; i < positions.Length; i++)
		{
			positions[i].y = Mathf.Sin(fakeTime * ((FrequencyInTermsOfPi) ? frequency * Mathf.PI : frequency)) * ((AmplitudeInTermsOfPi) ? amplitude * Mathf.PI : amplitude);
			positions[i].x = ((SeperateDistanceFromInterval) ? distance * i : fakeTime);
			fakeTime += ((IntervalInTermsOfPi) ? SineIntervals * Mathf.PI : SineIntervals);

			positions[i] = positions[i] * scalar;
			positions[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positions[i];
		}

		return positions;
	}
}
