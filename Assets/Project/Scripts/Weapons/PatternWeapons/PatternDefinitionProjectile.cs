using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
	public override void Initialize(Target target, int damage)
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
		
		[Tooltip("The direction that the spawning aims toward")]
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
		[Tooltip("Should the chosen direction be flipped.\r\nPoint Away -> Point Towards")]
		public bool flipDirection;

		[Tooltip("If the bullet should shoot away from spawnPlacement")]
		[AllowNesting]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "sameDirection", "inPointDirection", "inShootDirection")]
		public bool pointAwayFromSpawner;

		[Tooltip("If the bullet should shoot away from positionShootAwayFrom. If null, uses spawnplacement")]
		[AllowNesting]
		[HideIf(EConditionOperator.Or, "pointAwayFromSpawner", "sameDirection", "inPointDirection", "inShootDirection")]//
		public bool pointAwayFromPosition;

		[Tooltip("If all the bullets should fire in the same direction using pointDirection")]
		[AllowNesting]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "pointAwayFromSpawner", "inPointDirection", "inShootDirection")]
		public bool sameDirection;
		
		[Tooltip("Bullets should point within the point direction")]
		[AllowNesting]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "sameDirection", "pointAwayFromSpawner", "inShootDirection")]
		public bool inPointDirection;

		[Tooltip("Bullets should point toward the shoot direction")]
		[AllowNesting]
		[HideIf(EConditionOperator.Or, "pointAwayFromPosition", "sameDirection", "pointAwayFromSpawner", "inPointDirection")]
		public bool inShootDirection;

		[Header("Shooting")]
		[Tooltip("How the pattern shoots out")]
		public ShootType howshoot;

		[Tooltip("The direction that the bullets will shoot toward")]
		public Vector2 shootDirection;

		[Tooltip("Time between bullet shots")]
		public float shootDelay;
		
		[Tooltip("The projectiles that the pattern spawns")]
		[HideInInspector]public List<Projectile> projectiles;
		
		[Tooltip("shoot the projectiles in the reverse order they were spawned")]
		public bool shootReverse;
		
		[Tooltip("If the Pattern should SHOOT with the next pattern")]
		public bool shootWithNextPattern;
		
		[Tooltip("shoots spawned bullets from the center outwards")]
		public bool shootCenterOutwards;
	}

	[SerializeField, Header("Just This:")] Projectile bulletProjectile;

	[SerializeField] ShootPattern[] patterns;

	[SerializeField] bool aimOnShoot = false;

	Vector2 targetDirection;
	Vector2 targetPosition;

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
		transform.rotation *= Quaternion.Euler(0, 0, 90);
		StartCoroutine(SpawnBullets(0));
	}
	

	private IEnumerator SpawnBullets(int startpattern)
	{
		bool spawnwithnext = false;
		targetDirection = target.GetDirection();
		targetPosition = target.GetTargetPosition();

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
				positions = positions.OrderBy(p => Vector3.Distance(p, Vector3.zero)).ToArray();
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
					Vector2 direction = GetDirection(i, newproj.transform.position, false);
					float angle = InputUtils.GetAngle(direction);
					newproj.transform.rotation =  Quaternion.Euler(0, 0, angle);
				}
				else
				{
					newproj.transform.position = patterns[i].spawnPlacement.transform.position;
					Vector2 direction = GetDirection(i, newproj.transform.position, false);
					float angle = InputUtils.GetAngle(direction);
					newproj.transform.position += ( Quaternion.Euler(0, 0, angle) * positions[o]);
                }

				//newproj.transform.position = positions[o];

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
				if (aimOnShoot)
				{
					//Vector2 direction = (target.GetTargetPosition() - (Vector2)toshootprojs[i].transform.position);
					Vector2 direction = GetDirection(patternnumber, toshootprojs[i].transform.position, true);
					float angle = InputUtils.GetAngle(direction);
					toshootprojs[i].transform.rotation = Quaternion.Euler(0, 0, angle);
				}
				ShootProjectile(toshootprojs[i]);
				// patterns[patternnumber].projectiles[i].transform.SetParent(null);
				// Destroy(patterns[patternnumber].projectiles[i].gameObject);
			}
		}
	}

	private Vector2 GetDirection(int i, Vector3 projectileposition, bool shooting)
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
		else if (patterns[i].inShootDirection)
		{
			direction = patterns[i].shootDirection;
		}
		else
		{
			if (patterns[i].sameDirection)
			{
				direction = (aimOnShoot && shooting) ? target.GetDirection() : targetDirection;
			}
			else
			{
				direction = (((aimOnShoot && shooting) ? target.GetTargetPosition() : targetPosition) - (Vector2)projectileposition);
				//direction = target.GetDirection();
				//Debug.Log(direction);
			}
		}

		if (patterns[i].flipDirection)
		{
			direction *= -1;
		}

		return direction;
	}

	private void ShootProjectile(Projectile p)
	{
		p.transform.SetParent(null);
		p.Initialize(target, damage);
	}
}
