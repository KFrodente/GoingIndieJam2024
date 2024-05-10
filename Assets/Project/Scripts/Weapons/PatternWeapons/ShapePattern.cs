using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shape_Pattern", menuName = "Patterns/Shape")]
public class ShapePattern : Pattern
{
	[Header("Shape")]
	[SerializeField] Vector2[] positions;

	public override Vector3[] SpawnBullets(Vector3 direction)
	{
		Vector3[] positions = new Vector3[bulletAmount];



		return positions;
	}
}
