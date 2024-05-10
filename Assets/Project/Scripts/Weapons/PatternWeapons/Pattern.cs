using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : ScriptableObject
{
	[Header("Base Pattern")]
	[SerializeField] public Transform bulletPrefab;
	[SerializeField] public int bulletAmount = 4;
	[SerializeField] public float totalSpawnTime = 1.0f;
	[SerializeField] public float shootDelay = 0.5f;

	public abstract Vector3[] SpawnBullets(Vector3 direction);

	public Vector3[] Randomize(Vector3[] list)
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
