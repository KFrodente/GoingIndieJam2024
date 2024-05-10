using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWeapon : MonoBehaviour
{
	enum ShootType
	{
		ALLATONCE,
		ONEBYONEAFTERSPAWN,
		ONEBYONEDURINGSPAWN
	}

	[Serializable]
	struct ShootPattern
	{
		public Pattern pattern;
		public ShootType howshoot;
		public Vector2 pointDirection;
		public float delayToNext;
		public bool shootAwayFromSelf;
		public bool sameDirection;
		public bool withNextPattern;
	}

	[SerializeField] Transform bulletPrefab;
	//[SerializeField] float totalShootingTime = 0.5f;
	[SerializeField] Transform spawnPlacement;
	[SerializeField] Transform target;

	[SerializeField] bool shoot = false; // This is for debugging the shooting

	[SerializeField] ShootPattern[] patterns;

	[SerializeField] List<Transform> projectiles;

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
		//switch (pattern)
		//{
		//	case PatternType.CIRCLE:
		//		for (int i = 0; i < bulletAmount; i++)
		//		{
		//			float spawnAngle = (360 / bulletAmount) * i;
		//			Vector3 position = (Quaternion.Euler(0, 0, spawnAngle) * transform.right * circleRadius);
		//			projectiles.Add(Instantiate(bulletPrefab, spawnPlacement.position + position, Quaternion.Euler(0, 0, spawnAngle)));

		//			if (totalSpawnTime > 0 || i < bulletAmount - 1)
		//			{
		//				yield return new WaitForSeconds(totalSpawnTime / bulletAmount);
		//			}
		//		}
		//		break;
		//	case PatternType.SHAPE:
		//		for (int i = 0; i < bulletAmount; i++)
		//		{
		//			float spawnAngle = (360 / bulletAmount) * i;
		//			Vector3 position = (Quaternion.Euler(0, 0, spawnAngle) * transform.right * circleRadius);
		//			projectiles.Add(Instantiate(bulletPrefab, spawnPlacement.position + position, Quaternion.Euler(0, 0, spawnAngle)));

		//			if (totalSpawnTime > 0 || i < bulletAmount - 1)
		//			{
		//				yield return new WaitForSeconds(totalSpawnTime / bulletAmount);
		//			}
		//		}
		//		break;
		//	case PatternType.IMAGE:
		//		break;
		//}

		for (int i = 0; i < patterns.Length; i++)
		{
			int amountofbullets = patterns[i].pattern.bulletAmount;
			Vector3[] positions = patterns[i].pattern.SpawnBullets(patterns[i].pointDirection, spawnPlacement.position);
			for (int o = 0; o < positions.Length; o++)
			{
				var newproj = Instantiate((patterns[i].pattern.bulletPrefab == null) ? bulletPrefab : patterns[i].pattern.bulletPrefab, positions[o], Quaternion.identity);

				if (patterns[i].shootAwayFromSelf)
				{
					newproj.transform.rotation = Quaternion.Euler(0, 0, (360 / amountofbullets) * o);
				} 
				else
				{
					Vector3 direction;
					if (patterns[i].sameDirection)
					{
						direction = (target.position - spawnPlacement.position);
					}
					else
					{
						direction = (target.position - newproj.position);
					}
					float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
					newproj.transform.rotation = Quaternion.Euler(0,0, angle);
				}

				projectiles.Add(newproj);

				if (patterns[i].pattern.totalSpawnTime > 0 || i < amountofbullets - 1)
				{
					yield return new WaitForSeconds(patterns[i].pattern.totalSpawnTime / amountofbullets);
				}
			}

			float additionalDelay = 0;

			if (i+1 < patterns.Length && patterns[i].withNextPattern)
			{
				additionalDelay += patterns[i + 1].pattern.totalSpawnTime + patterns[i+1].pattern.shootDelay;
			}

			StartCoroutine(ShootBullets(i, additionalDelay));

			yield return new WaitForSeconds(patterns[i].delayToNext);
		}
	}

	private IEnumerator ShootBullets(int patternnumber, float additionaldelay)
	{
		List<Transform> toshootprojs = new List<Transform>();
		foreach (var proj in projectiles)
		{
			toshootprojs.Add(proj.transform);
		}
		projectiles.Clear();

		yield return new WaitForSeconds(additionaldelay);

		for (int i = 0; i < toshootprojs.Count; i++)
		{
			if (toshootprojs != null)
			{
				if (i == 0 || patterns[patternnumber].howshoot == ShootType.ONEBYONEAFTERSPAWN || patterns[patternnumber].howshoot == ShootType.ONEBYONEDURINGSPAWN)
				{
					yield return new WaitForSeconds(patterns[patternnumber].pattern.shootDelay);
				}
				// this is the place where the projectiles will actually be shot
				Destroy(toshootprojs[i].gameObject);
			}
		}
	}
}
