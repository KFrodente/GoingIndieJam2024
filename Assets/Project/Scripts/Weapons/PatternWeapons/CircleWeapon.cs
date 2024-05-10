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
		/// <summary>
		/// The actual pattern scriptable object to use
		/// </summary>
		public Pattern pattern;
		/// <summary>
		/// How the pattern shoots out
		/// </summary>
		public ShootType howshoot;
		/// <summary>
		/// The direction that the spawning originates from<br/>
		///		Overrides:<br/>
		///		ONEBYONEDURINGSPAWN -> shootWithNextPattern
		/// </summary>
		public Vector2 pointDirection;
		/// <summary>
		/// Delay between this pattern spawning and the next pattern spawning
		/// </summary>
		public float delayToNext;
		/// <summary>
		/// The position where the bullet pattern will spawn
		/// </summary>
		public Transform spawnPlacement;
		/// <summary>
		/// If the bullet should shoot away from spawnPlacement<br/>
		///		Overrides:<br/>
		///		sameDirection
		/// </summary>
		public bool shootAwayFromSelf;
		/// <summary>
		/// If all the bullets should fire in the same direction using pointDirection
		/// </summary>
		public bool sameDirection;
		/// <summary>
		/// If the Pattern should SPAWN with the next pattern.
		/// </summary>
		public bool spawnWithNextPattern;
		/// <summary>
		/// If the Pattern should SHOOT with the next pattern.
		/// </summary>
		public bool shootWithNextPattern;
		/// <summary>
		/// The projectiles that the pattern spawns
		/// </summary>
		public List<Transform> projectiles;
	}

	[SerializeField] Transform bulletPrefab;
	//[SerializeField] float totalShootingTime = 0.5f;
	[SerializeField] Transform target;

	[SerializeField] bool shoot = false; // This is for debugging the shooting

	[SerializeField] ShootPattern[] patterns;

	private void Start()
	{
		for (int i = 0; i < patterns.Length; i++)
		{
			if (patterns[i].spawnPlacement == null)
			{
				patterns[i].spawnPlacement = transform;
			}
		}
	}

	private void Update()
	{
		if (shoot && bulletPrefab)
		{
			shoot = false;

			StartCoroutine(SpawnBullets(0));
		}
	}

	private IEnumerator SpawnBullets(int startpattern)
	{
		bool spawnwithnext = false;

		for (int i = startpattern; i < patterns.Length; i++)
		{
			if (i + 1 < patterns.Length && patterns[i].spawnWithNextPattern)
			{
				StartCoroutine(SpawnBullets(i+1));
				spawnwithnext = true;
			}

			int amountofbullets = patterns[i].pattern.bulletAmount;
			Vector3[] positions = patterns[i].pattern.SpawnBullets(patterns[i].pointDirection, patterns[i].spawnPlacement.position);
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
						direction = (target.position - (patterns[i].spawnPlacement.position + (Vector3)patterns[i].pointDirection));
					}
					else
					{
						direction = (target.position - newproj.position);
					}
					float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
					newproj.transform.rotation = Quaternion.Euler(0,0, angle);
				}

				patterns[i].projectiles.Add(newproj);

				if (patterns[i].howshoot == ShootType.ONEBYONEDURINGSPAWN)
				{
					StartCoroutine(ShootBullets(i, patterns[i].pattern.shootDelay));
				}

				if (patterns[i].pattern.totalSpawnTime > 0 || i < amountofbullets - 1)
				{
					yield return new WaitForSeconds(patterns[i].pattern.totalSpawnTime / amountofbullets);
				}
			}

			float additionalDelay = 0;

			if (i+1 < patterns.Length && patterns[i].shootWithNextPattern)
			{
				for (int o = i+1; o < patterns.Length; o++)
				{
					if (!patterns[o-1].shootWithNextPattern)
					{
						break;
					}
					if (patterns[o - 1].spawnWithNextPattern)
					{
						continue;
					}
					additionalDelay += patterns[o].pattern.totalSpawnTime + patterns[o].pattern.shootDelay + patterns[o].delayToNext;
				}
				Debug.Log("Pattern " + i + " is waiting an additional " + additionalDelay + " seconds");
			}
			
			StartCoroutine(ShootBullets(i, additionalDelay));

			if (spawnwithnext)
			{
				break;
			}

			yield return new WaitForSeconds(patterns[i].delayToNext);
		}
	}

	private IEnumerator ShootBullets(int patternnumber, float additionaldelay)
	{
		yield return new WaitForSeconds(additionaldelay);

		for (int i = 0; i < patterns[patternnumber].projectiles.Count; i++)
		{
			if (patterns[patternnumber].projectiles != null)
			{
				if (i == 0 || patterns[patternnumber].howshoot == ShootType.ONEBYONEAFTERSPAWN || patterns[patternnumber].howshoot == ShootType.ONEBYONEDURINGSPAWN)
				{
					yield return new WaitForSeconds(patterns[patternnumber].pattern.shootDelay);
				}
				// this is the place where the projectiles will actually be shot
				Destroy(patterns[patternnumber].projectiles[i].gameObject);
			}
		}

		patterns[patternnumber].projectiles.Clear();
	}
}
