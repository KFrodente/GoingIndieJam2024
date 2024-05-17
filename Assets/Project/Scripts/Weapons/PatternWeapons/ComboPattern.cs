using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo_Pattern", menuName = "Patterns/Combo")]
public class ComboPattern : Pattern
{
	[Header("Combo")]
	[Tooltip("The base pattern the added pattern is applied to")]
	[SerializeField, Expandable] Pattern basePattern;
	[Tooltip("The pattern that is added onto the base")]
	[SerializeField, Expandable] Pattern addedPattern;

	[Tooltip("How many bullets shoul be skipped in the base pattern before the added pattern occurs")]
	[SerializeField] int skippedBullets;
	int skippedBulletsCount;
	[Tooltip("If the skipped bullets should start at 0 or bulletcount")]
	[SerializeField] bool skipFirstbullet;
	[Tooltip("The direction the added pattern spawns in")]
	[SerializeField, HideIf("pointAwayFromZero")] Vector2 spawnDirection;
	[Tooltip("if the added pattern should point away from spawn")]
	[SerializeField] bool pointAwayFromZero;
	[Tooltip("if the added pattern should point towards the spawn")]
	[SerializeField] bool pointTowardsZero;
	[Tooltip("if the base pattern should be ignored in the final pattern")]
	[SerializeField] bool dontSpawnBasePattern;

	public override Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar)
	{
		Vector3[] basepositions = basePattern.SpawnBullets(direction, scalar);
		List<Vector3> positionsToReturn = new List<Vector3>();

		Vector3 usedDirection = direction;
		if (spawnDirection != Vector2.zero)
		{
			usedDirection = spawnDirection;
		}

		skippedBulletsCount = (!skipFirstbullet) ? 0 : skippedBullets;
		for (int i = 0; i < basepositions.Length; i++)
		{
			if (!dontSpawnBasePattern)
			{
				positionsToReturn.Add(basepositions[i]);
			}

			skippedBulletsCount--;
			if (skippedBulletsCount <= 0)
			{
				if (pointTowardsZero)
				{
					usedDirection = -basepositions[i];
				}
				else if (pointAwayFromZero)
				{
					usedDirection = basepositions[i];
				}

				Vector3[] added = addedPattern.SpawnBullets(usedDirection, scalar);
				for (int o = 0; o <  added.Length; o++)
				{
					added[o] += basepositions[i];
					positionsToReturn.Add(added[o]);
				}
				skippedBulletsCount = skippedBullets;
			}
		}

		for (int i = 0; i < positionsToReturn.Count; i++)
		{
			positionsToReturn[i] = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) * positionsToReturn[i];
		}

		return positionsToReturn.ToArray();
	}
}
