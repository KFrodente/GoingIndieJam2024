using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int roomPoints;

    public bool enemiesCleared = false;

    public List<GameObject> spawnedEnemies = new();

    private void Start()
    {
        FloorStatsSO fs = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum];
        roomPoints = Random.Range(fs.roomPoints.x, fs.roomPoints.y);
    }

    public void SpawnEnemies(List<Vector2Int> usableSpawnPositions, Vector2 roomOffset)
    {
        if (!enemiesCleared)
        {
            while (roomPoints > 1 && usableSpawnPositions.Count > 0)
            {

                int allocatedPoints = Random.Range(1, Mathf.Min(4, roomPoints));

                roomPoints -= allocatedPoints;

                int index = Random.Range(0, usableSpawnPositions.Count);
                Vector2 pickedPos = usableSpawnPositions[index];


                GameObject enemy = Instantiate(GetEnemy(allocatedPoints), pickedPos + roomOffset, transform.rotation);
                usableSpawnPositions.RemoveAt(index);
                spawnedEnemies.Add(enemy);
            }
        }
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

    public void CheckRoomCleared()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] != null)
            {
                return;
            }
        }

        enemiesCleared = true;
    }
}
