using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerProjectile : Projectile
{
	[Header("Spawner Properties")]
	[SerializeField] private List<GameObject> charactersToSpawn;
	[SerializeField] private float timeToSpawn;
	private float projectileSpawnTime;

	public override void Initialize(Target target, int damage)
	{
		base.Initialize(target, damage);
		projectileSpawnTime = Time.time + timeToSpawn;
	}

	private void Update()
	{
		base.Update();
		if(Time.time > projectileSpawnTime) 
		{
			SpawnCharacter();
		}
	}

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		//base.OnTriggerEnter2D(other);
		if (!initialized) return;
		SpawnCharacter();
	}

	private void SpawnCharacter()
	{
		int randomIndex = Random.Range(0, charactersToSpawn.Count);

		Instantiate(charactersToSpawn[randomIndex], this.transform.position, Quaternion.identity);

		Destroy(gameObject);
	}
}
