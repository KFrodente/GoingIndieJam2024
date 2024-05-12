using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : ScriptableObject
{
	[Header("Base Pattern")]
	[SerializeField] public Projectile bulletPrefab;
	[SerializeField] protected int bulletAmount = 4;

	public abstract Vector3[] SpawnBullets(Vector3 direction, Vector2 scalar);

	public static Vector3[] Randomize(Vector3[] list)
	{
		for (int i = 0; i < list.Length; i++)
		{
			Vector3 temp = list[i];
			int random = UnityEngine.Random.Range(0, list.Length);
			list[i] = list[random];
			list[random] = temp;
		}

		return list;
	}
}
