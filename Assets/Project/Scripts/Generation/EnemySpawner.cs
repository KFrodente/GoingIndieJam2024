using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int roomPoints;

    public bool enemiesCleared = false;

    public int spawnedEnemies = 0;

    private void Start()
    {
        FloorStatsSO fs = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum];
        roomPoints = Random.Range(fs.roomPoints.x, fs.roomPoints.y);
    }

    public void SpawnEnemies(List<Vector2Int> usableSpawnPositions, Vector2 roomOffset)
    {
        Debug.Log("Should be spawning Enemies");
        if (!enemiesCleared)
        {
            while (roomPoints > 1 && usableSpawnPositions.Count > 0)
            {

                int allocatedPoints = Random.Range(1, Mathf.Min(4, roomPoints));

                roomPoints -= allocatedPoints;

                int index = Random.Range(0, usableSpawnPositions.Count);
                Vector2 pickedPos = usableSpawnPositions[index];


                Instantiate(GetEnemy(allocatedPoints), pickedPos + roomOffset + (Vector2.one / 2), transform.rotation, transform);
                usableSpawnPositions.RemoveAt(index);
                spawnedEnemies++;
            }
        }
        StartCoroutine(EnemiesCleared());
    }

    private GameObject GetEnemy(int points)
    {
        Debug.Log(points);
        switch(points)
        {
            case 1:
                return EnemyPicker.instance.GetEasyEnemy();
            case 2:
                return EnemyPicker.instance.GetMediumEnemy();
            case 3:
                return EnemyPicker.instance.GetHardEnemy();
            default:
                return null;
        }
    }


    private IEnumerator EnemiesCleared()
    {
        yield return new WaitUntil(() => spawnedEnemies <= 0);

        gameObject.GetComponent<WalkerGenerator>().SetPortalActivity(true);
    }
}
