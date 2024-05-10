using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPicker : MonoBehaviour
{
    public static EnemyPicker instance;


    [SerializeField] private List<GameObject> rareEnemies = new();
    [SerializeField] private List<GameObject> easyEnemies = new();
    [SerializeField] private List<GameObject> mediumEnemies = new();
    [SerializeField] private List<GameObject> hardEnemies = new();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public GameObject GetEasyEnemy()
    {
        if (Random.Range(0, 1000) == 1)
        {
            Debug.Log("Spawned rare enemy!");
            return rareEnemies[Random.Range(0, rareEnemies.Count)];
        }
        return easyEnemies[Random.Range(0, easyEnemies.Count)];
    }

    public GameObject GetMediumEnemy()
    {
        if (Random.Range(0, 1000) == 1)
        {
            Debug.Log("Spawned rare enemy!");
            return rareEnemies[Random.Range(0, rareEnemies.Count)];
        }
        return mediumEnemies[Random.Range(0, mediumEnemies.Count)];
    }

    public GameObject GetHardEnemy()
    {
        if (Random.Range(0, 1000) == 1)
        {
            Debug.Log("Spawned rare enemy!");
            return rareEnemies[Random.Range(0, rareEnemies.Count)];
        }
        return hardEnemies[Random.Range(0, hardEnemies.Count)];
    }
}
