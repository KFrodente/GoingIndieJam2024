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
		/// Point in space that the projectiles shoot away from
		/// </summary>
		public Transform positionShootAwayFrom;
		/// <summary>
		/// If the spawned projectiles should be parented to the placements
		/// </summary>
		public bool dontParentToSpawner;
		/// <summary>
		/// If the bullet should shoot away from transform.position<br/>
		///		Overrides:<br/>
		///		sameDirection
		/// </summary>
		public bool shootAwayFromSpawner;
		/// <summary>
		/// If the bullet should shoot away from positionShootAwayFrom. If null, uses spawnplacement<br/>
		///		Overrides:<br/>
		///		sameDirection<br/>
		///		shootAwayFromSpawner
		/// </summary>
		public bool shootAwayFromPosition;
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

	[SerializeField] Transform target;

	/// <summary>
	/// When set to true, will initiate the shoot functions
	/// </summary>
	[SerializeField] bool shoot = false;


	[SerializeField] ShootPattern[] patterns;

	private void Start()
	{
		for (int i = 0; i < patterns.Length; i++)
		{
			if (patterns[i].spawnPlacement == null)
			{
				patterns[i].spawnPlacement = transform;
			}
			patterns[i].pointDirection = patterns[i].pointDirection.normalized;
		}
	}

	public void Shoot()
	{
		shoot = true;
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

			//int amountofbullets = patterns[i].pattern.bulletAmount;
			Vector3[] positions = patterns[i].pattern.SpawnBullets(patterns[i].pointDirection);
			for (int o = 0; o < positions.Length; o++)
			{
				var newproj = Instantiate((patterns[i].pattern.bulletPrefab == null) ? bulletPrefab : patterns[i].pattern.bulletPrefab);

				if (!patterns[i].dontParentToSpawner)
				{
					newproj.SetParent(patterns[i].spawnPlacement);
				}

				newproj.localPosition = positions[o];

				Vector3 direction;
				if (patterns[i].shootAwayFromPosition)
				{
					direction = (newproj.position - ((patterns[i].positionShootAwayFrom == null) ? patterns[i].spawnPlacement.position : patterns[i].positionShootAwayFrom.position));
				}
				else if(patterns[i].shootAwayFromSpawner)
				{
					direction = (newproj.position - transform.position);
				}
				else
				{
					if (patterns[i].sameDirection)
					{
						direction = (Vector3)patterns[i].pointDirection;
					}
					else
					{
						direction = (target.position - newproj.position);
						Debug.Log(direction);
					}
				}
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				newproj.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Quaternion.Euler(0,0, angle);

				patterns[i].projectiles.Add(newproj);

				if (patterns[i].howshoot == ShootType.ONEBYONEDURINGSPAWN)
				{
					StartCoroutine(ShootBullets(i, patterns[i].pattern.shootDelay));
				}

				if (patterns[i].pattern.totalSpawnTime > 0 || i < positions.Length - 1)
				{
					yield return new WaitForSeconds(patterns[i].pattern.totalSpawnTime / positions.Length);
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
		List<Transform> toshootprojs = new List<Transform>();
		foreach (var proj in patterns[patternnumber].projectiles)
		{
			toshootprojs.Add(proj.transform);
		}
		patterns[patternnumber].projectiles.Clear();

		yield return new WaitForSeconds(additionaldelay);

		for (int i = 0; i < toshootprojs.Count; i++)
		{
			if (toshootprojs[i] != null)
			{
				if (i == 0 || patterns[patternnumber].howshoot == ShootType.ONEBYONEAFTERSPAWN)
				{
					yield return new WaitForSeconds(patterns[patternnumber].pattern.shootDelay);
				}
				// this is the place where the projectiles will actually be shot

				toshootprojs[i].SetParent(null);
				//Vector2 direction = (target.position - patterns[patternnumber].projectiles[i].position);
				//float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				//patterns[patternnumber].projectiles[i].transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Quaternion.Euler(0, 0, angle);
				//Destroy(patterns[patternnumber].projectiles[i].gameObject);
			}
		}
	}
}
