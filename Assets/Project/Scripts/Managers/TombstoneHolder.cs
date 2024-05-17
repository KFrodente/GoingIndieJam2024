using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TombstoneHolder : MonoBehaviour
{
    public static TombstoneHolder instance;

    public List<GameObject> basicCharacter = new();
    public List<GameObject> rareCharacter = new();
    public List<GameObject> immenseCharacter = new();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public GameObject GetTombstone(int roomCost)
    {
        float addedChance = roomCost / (float)10;
        float chance = Random.Range(0.0f, 100.0f);

        if (chance <= 5 + addedChance) return GetImmenseCharacter();
        else if (chance <= 30 + addedChance) return GetRareCharacter();
        else return GetBasicCharacter();
    }


    private GameObject GetBasicCharacter()
    {
        return basicCharacter[Random.Range(0, basicCharacter.Count)];
    }

    private GameObject GetRareCharacter()
    {
        return rareCharacter[Random.Range(0, rareCharacter.Count)];
    }

    private GameObject GetImmenseCharacter()
    {
        return immenseCharacter[Random.Range(0, immenseCharacter.Count)];
    }
}
