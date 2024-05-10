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

	public abstract Vector3[] SpawnBullets(Vector3 direction, Vector3 spawnPlacement);
}
