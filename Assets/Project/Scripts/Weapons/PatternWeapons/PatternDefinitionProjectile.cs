using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PatternDefinitionProjectile : Projectile
{
	protected override void OnTriggerEnter2D(Collider2D other)
	{
	}
	protected override void Update()
	{
	}
	public virtual void Initialize(Target target, int damage)
	{
		spawnTime = Time.time;
		this.damage = damage;
		this.target = target;
		initialized = true;
	}
	
	enum ShootType
	{
		ALLATONCE,
		ONEBYONEAFTERSPAWN,
		ONEBYONEDURINGSPAWN
	}

	[Serializable]
	struct ShootPattern
	{
		[Header("Spawning")]
		/// <summary>
		/// The actual pattern scriptable object to use
		/// </summary>
		public Pattern pattern;
		/// <summary>
		/// Time to spawn the full pattern
		/// </summary>
		public float totalSpawnTime;
		/// <summary>
		/// The direction that the spawning originates from
		/// </summary>
		public Vector2 pointDirection;
		/// <summary>
		/// Delay between this pattern spawning and the next pattern spawning
		/// </summary>
		public float delayToNext;
		/// <summary>
		/// Scale the pattern by this amount on the x and y
		/// </summary>
		public Vector2 scale;
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
		/// Bullets should point within the point direction
		/// </summary>
		public bool inPointDirection;
		/// <summary>
		/// randomized the spawn order of the projectiles
		/// </summary>
		public bool randomize;
		/// <summary>
		/// If the Pattern should SPAWN with the next pattern.
		/// </summary>
		public bool spawnWithNextPattern;
		[Header("Shooting")]
		/// <summary>
		/// How the pattern shoots out<br/>
		///		Overrides:<br/>
		///		ONEBYONEDURINGSPAWN -> shootWithNextPattern
		/// </summary>
		public ShootType howshoot;
		/// <summary>
		/// Time between bullet shots
		/// </summary>
		public float shootDelay;
		/// <summary>
		/// The projectiles that the pattern spawns
		/// </summary>
		public List<Projectile> projectiles;
		/// <summary>
		/// shoot the projectiles in the reverse order they were spawned
		/// </summary>
		public bool shootReverse;
		/// <summary>
		/// If the Pattern should SHOOT with the next pattern.
		/// </summary>
		public bool shootWithNextPattern;
	}

	[SerializeField, Header("Just This:")] Projectile bulletProjectile;


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
			if (patterns[i].pointDirection == Vector2.zero) patterns[i].pointDirection = Vector2.right;
			patterns[i].pointDirection = patterns[i].pointDirection.normalized;
		}
		StartCoroutine(SpawnBullets(0));
	}
	

	private IEnumerator SpawnBullets(int startpattern)
	{
		bool spawnwithnext = false;
		Vector2 possiblesamedirection = target.GetTargetPosition() - (Vector2)transform.position;

		for (int i = startpattern; i < patterns.Length; i++)
		{
			if (i + 1 < patterns.Length && patterns[i].spawnWithNextPattern)
			{
				StartCoroutine(SpawnBullets(i+1));
				spawnwithnext = true;
			}

			//int amountofbullets = patterns[i].pattern.bulletAmount;
			Vector3[] positions = patterns[i].pattern.SpawnBullets(patterns[i].pointDirection, patterns[i].scale);

			if (patterns[i].randomize)
			{
				positions = Pattern.Randomize(positions);
			}

			for (int o = 0; o < positions.Length; o++)
			{
				Projectile newproj = (Instantiate((patterns[i].pattern.bulletPrefab == null) ? bulletProjectile.gameObject : patterns[i].pattern.bulletPrefab.gameObject)).GetComponent<Projectile>();

				if (!patterns[i].dontParentToSpawner)
				{
					newproj.transform.SetParent(patterns[i].spawnPlacement);
				}

				newproj.transform.localPosition = positions[o];

				Vector2 direction;
				if (patterns[i].shootAwayFromPosition)
				{
					direction = (newproj.transform.position - ((patterns[i].positionShootAwayFrom == null) ? 
						patterns[i].spawnPlacement.position : 
						patterns[i].positionShootAwayFrom.position));
				}
				else if(patterns[i].shootAwayFromSpawner)
				{
					direction = (newproj.transform.position - transform.position);
				}
				else if (patterns[i].inPointDirection)
				{
					direction = patterns[i].pointDirection;
				} else
				{
					if (patterns[i].sameDirection)
					{
						direction = possiblesamedirection;
					}
					else
					{
						direction = target.GetDirection();
						Debug.Log(direction);
					}
				}
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				newproj.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Quaternion.Euler(0,0, angle);

				patterns[i].projectiles.Add(newproj);

				if (patterns[i].howshoot == ShootType.ONEBYONEDURINGSPAWN)
				{
					StartCoroutine(ShootBullets(i, patterns[i].shootDelay));
				}

				if (patterns[i].totalSpawnTime > 0 && i < positions.Length - 1)
				{
					yield return new WaitForSeconds(Mathf.Max(patterns[i].totalSpawnTime / positions.Length, Time.deltaTime));
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
					additionalDelay += patterns[o].totalSpawnTime + patterns[o].shootDelay + patterns[o].delayToNext;
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
		// List<Projectile> toshootprojs = new List<Projectile>();
		// foreach (var proj in patterns[patternnumber].projectiles)
		// {
		// 	if (proj != null)
		// 	{
		// 		toshootprojs.Add(proj);	
		// 	}
		// }
		// patterns[patternnumber].projectiles.Clear();
		// if (patterns[patternnumber].shootReverse)
		// {
		// 	toshootprojs.Reverse();
		// }

		yield return new WaitForSeconds(additionaldelay);

		for (int i = 0; i < patterns[patternnumber].projectiles.Count; i++)
		{
			if (patterns[patternnumber].projectiles[i] != null)
			{
				if (patterns[patternnumber].shootDelay > 0 && (i == 0 || patterns[patternnumber].howshoot == ShootType.ONEBYONEAFTERSPAWN))
				{
					yield return new WaitForSeconds(Mathf.Max(patterns[patternnumber].shootDelay, Time.deltaTime));
				}
				// this is the place where the projectiles will actually be shot
				Vector2 direction = (target.GetTargetPosition() - (Vector2)patterns[patternnumber].projectiles[i].transform.position);
				float angle = InputUtils.GetAngle(direction);
				patterns[patternnumber].projectiles[i].transform.rotation = Quaternion.Euler(0, 0, angle);
				ShootProjectile(patterns[patternnumber].projectiles[i]);
				// patterns[patternnumber].projectiles[i].transform.SetParent(null);
				// Destroy(patterns[patternnumber].projectiles[i].gameObject);
			}
		}
	}

	private void ShootProjectile(Projectile p)
	{
		p.transform.SetParent(null);
		p.Initialize(target, damage);
	}
}
