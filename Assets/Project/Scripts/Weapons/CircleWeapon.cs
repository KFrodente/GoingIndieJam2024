using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWeapon : MonoBehaviour
{
	enum PatternType
	{
		CIRCLE,
		SHAPE,
		IMAGE
	}

	[SerializeField] PatternType pattern = PatternType.CIRCLE;

	enum ShootType
	{
		ALLATONCE,
		ALLATONCEOFFSET,
		ONEBYONEAFTERSPAWN,
		ONEBYONEDURINGSPAWN
	}

	[SerializeField] ShootType howshoot = ShootType.ALLATONCE;

	[SerializeField] Transform bulletPrefab;
	[SerializeField] bool shootAwayFromSelf = false;
	[SerializeField] int bulletAmount = 4;
	[SerializeField] float totalSpawnTime = 1.0f;
	[SerializeField] float shootDelay = 0.5f;
	//[SerializeField] float totalShootingTime = 0.5f;
	[SerializeField] Transform spawnPlacement;

	[SerializeField] List<Transform> projectiles;

	[SerializeField] bool shoot = false; // This is for debugging the shooting


	[Header("Circle")]
	[SerializeField] float circleRadius = 1.0f;

	private void Update()
	{
		if (shoot && bulletPrefab)
		{
			shoot = false;

			StartCoroutine(SpawnBullets());
		}
	}

	private IEnumerator SpawnBullets()
	{
		for (int i = 0; i < bulletAmount; i++)
		{
			float spawnAngle = (360 / bulletAmount) * i;
			Vector3 position = (Quaternion.Euler(0, 0, spawnAngle) * transform.right * circleRadius);
			projectiles.Add(Instantiate(bulletPrefab, spawnPlacement.position + position, Quaternion.Euler(0, 0, spawnAngle)));

			if (totalSpawnTime > 0 || i < bulletAmount - 1)
			{
				yield return new WaitForSeconds(totalSpawnTime / bulletAmount);
			}
		}

		StartCoroutine(ShootBullets());
	}

	private IEnumerator ShootBullets()
	{
		for (int i = 0; i < projectiles.Count; i++)
		{
			if (i == 0 || howshoot == ShootType.ONEBYONEAFTERSPAWN || howshoot == ShootType.ONEBYONEDURINGSPAWN)
			{
				yield return new WaitForSeconds(shootDelay);
			}
			// this is the place where the projectiles will actually be shot
			Destroy(projectiles[i].gameObject);
		}

		projectiles.Clear();
	}
}
