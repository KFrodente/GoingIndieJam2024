using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
		[Expandable]
		[Tooltip("The actual pattern scriptable object to use")]
		public Pattern pattern;

		[Tooltip("Time to spawn the full pattern")]
		public float totalSpawnTime;
		
		[Tooltip("The direction that the spawning originates from")]
		public Vector2 pointDirection;
		
		[Tooltip("Delay between this pattern spawning and the next pattern spawning")]
		public float delayToNext;
		
		[Tooltip("Scale the pattern by this amount on the x and y")]
		public Vector2 scale;
		
		[Tooltip("The position where the bullet pattern will spawn")]
		public Transform spawnPlacement;
		
		[Tooltip("Point in space that the projectiles shoot away from")]
		public Transform positionShootAwayFrom;
		
		[Tooltip("If the spawned projectiles should be parented to the placements")]
		public bool dontParentToSpawner;

		[Tooltip("randomized the spawn order of the projectiles")]
		public bool randomize;

		[Tooltip("If the Pattern should SPAWN with the next pattern")]
		public bool spawnWithNextPattern;

		[Tooltip("spawns bullets from the center outwards")]
		public bool spawnCenterOutwards;

		[Header("PointDirection")]
		[Tooltip("If the bullet should shoot away from transform.position")]
		[AllowNesting]
		[Foldout("Point")]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "sameDirection", "inPointDirection")]
		public bool pointAwayFromSpawner;
		
		[Tooltip("If the bullet should shoot away from positionShootAwayFrom. If null, uses spawnplacement")]
		[AllowNesting]
		[Foldout("Point")]
		[HideIf(EConditionOperator.Or, "pointAwayFromSpawner", "sameDirection", "inPointDirection")]//
		public bool pointAwayFromPosition;
		
		[Tooltip("If all the bullets should fire in the same direction using pointDirection")]
		[AllowNesting]
		[Foldout("Point")]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "pointAwayFromSpawner", "inPointDirection")]
		public bool sameDirection;
		
		[Tooltip("Bullets should point within the point direction")]
		[AllowNesting]
		[Foldout("Point")]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "sameDirection", "pointAwayFromSpawner")]
		public bool inPointDirection;
		
		[Header("Shooting")]
		[Tooltip("How the pattern shoots out")]
		public ShootType howshoot;
		
		[Tooltip("Time between bullet shots")]
		public float shootDelay;
		
		[Tooltip("The projectiles that the pattern spawns")]
		public List<Projectile> projectiles;
		
		[Tooltip("shoot the projectiles in the reverse order they were spawned")]
		public bool shootReverse;
		
		[Tooltip("If the Pattern should SHOOT with the next pattern")]
		public bool shootWithNextPattern;
		
		[Tooltip("shoots spawned bullets from the center outwards")]
		public bool shootCenterOutwards;
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
			float starttime = Time.time;

			if (i + 1 < patterns.Length && patterns[i].spawnWithNextPattern)
			{
				StartCoroutine(SpawnBullets(i+1));
				spawnwithnext = true;
			}

			//int amountofbullets = patterns[i].pattern.bulletAmount;
			Vector3[] positions = patterns[i].pattern.SpawnBullets(patterns[i].pointDirection, patterns[i].scale);

			if (patterns[i].spawnCenterOutwards)
			{
				positions = positions.OrderBy(p => Vector3.Distance(p, patterns[i].spawnPlacement.localPosition)).ToArray();
			}
			if (patterns[i].randomize)
			{
				positions = Pattern.Randomize(positions);
			}

			for (int o = 0; o < positions.Length; o++)
			{
				Projectile newproj = (Instantiate(((patterns[i].pattern.bulletPrefab == null) ? bulletProjectile.gameObject : 
					patterns[i].pattern.bulletPrefab.gameObject)).GetComponent<Projectile>());

				if (!patterns[i].dontParentToSpawner)
				{
					newproj.transform.SetParent(patterns[i].spawnPlacement.transform, false);
                    newproj.transform.localPosition = positions[o];
                } else
				{
                    newproj.transform.position = patterns[i].spawnPlacement.transform.position + positions[o];
                }

				//newproj.transform.position = positions[o];

				Vector2 direction = GetDirection(i, newproj.transform.position, possiblesamedirection);
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				newproj.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z) * Quaternion.Euler(0,0, angle);

				patterns[i].projectiles.Add(newproj);

				if (patterns[i].howshoot == ShootType.ONEBYONEDURINGSPAWN)
				{
					StartCoroutine(ShootBullets(i, patterns[i].shootDelay));
				}

				float timeneeded = patterns[i].totalSpawnTime / positions.Length;
				if (timeneeded * o > (Time.time - starttime) && patterns[i].totalSpawnTime > 0 && i < positions.Length - 1)
				{
					yield return new WaitForSeconds(timeneeded);
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
				//Debug.Log("Pattern " + i + " is waiting an additional " + additionalDelay + " seconds");
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
		List<Projectile> toshootprojs = new List<Projectile>();
		foreach (var proj in patterns[patternnumber].projectiles)
		{
			if (proj != null)
			{
				toshootprojs.Add(proj);
			}
		}
		patterns[patternnumber].projectiles.Clear();
		if (patterns[patternnumber].shootCenterOutwards)
		{
			toshootprojs = toshootprojs.OrderBy(p => Vector3.Distance(p.transform.position, patterns[patternnumber].spawnPlacement.position)).ToList();
		}
		if (patterns[patternnumber].shootReverse)
		{
			toshootprojs.Reverse();
		}

		yield return new WaitForSeconds(additionaldelay);

		for (int i = 0; i < toshootprojs.Count; i++)
		{
			if (toshootprojs[i] != null)
			{
				if (patterns[patternnumber].shootDelay > 0 && (i == 0 || patterns[patternnumber].howshoot == ShootType.ONEBYONEAFTERSPAWN))
				{
					yield return new WaitForSeconds(patterns[patternnumber].shootDelay);
				}
				// this is the place where the projectiles will actually be shot
				//Vector2 direction = (target.GetTargetPosition() - (Vector2)toshootprojs[i].transform.position);
				Vector2 direction = GetDirection(patternnumber, toshootprojs[i].transform.position, (target.GetTargetPosition() - (Vector2)toshootprojs[i].transform.position));
				float angle = InputUtils.GetAngle(direction);
				toshootprojs[i].transform.rotation = Quaternion.Euler(0, 0, angle);
				ShootProjectile(toshootprojs[i]);
				// patterns[patternnumber].projectiles[i].transform.SetParent(null);
				// Destroy(patterns[patternnumber].projectiles[i].gameObject);
			}
		}
	}

	private Vector2 GetDirection(int i, Vector3 projectileposition, Vector2 possiblesamedirection)
	{
		Vector2 direction;
		if (patterns[i].pointAwayFromPosition)
		{
			direction = (projectileposition - ((patterns[i].positionShootAwayFrom == null) ?
				patterns[i].spawnPlacement.position :
				patterns[i].positionShootAwayFrom.position));
		}
		else if (patterns[i].pointAwayFromSpawner)
		{
			direction = (projectileposition - transform.position);
		}
		else if (patterns[i].inPointDirection)
		{
			direction = patterns[i].pointDirection;
		}
		else
		{
			if (patterns[i].sameDirection)
			{
				direction = possiblesamedirection;
			}
			else
			{
				direction = target.GetDirection();
				//Debug.Log(direction);
			}
		}

		return direction;
	}

	private void ShootProjectile(Projectile p)
	{
		p.transform.SetParent(null);
		p.Initialize(target, damage);
	}
}
