using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEssenceHolder : MonoBehaviour
{
    public static SpiritEssenceHolder instance;

    public List<GameObject> SSRankEssence = new();
    public List<GameObject> SRankEssence = new();
    public List<GameObject> ARankEssence = new();
    public List<GameObject> BRankEssence = new();
    public List<GameObject> CRankEssence = new();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public GameObject GetSpiritEssence(int roomCost)
    {
        float addedChance = roomCost / (float)10;
        float chance = Random.Range(0.0f, 100.0f);

        if (chance <= 1 + addedChance) return GetSSRankEssence();
        else if (chance <= 5 + addedChance) return GetSRankEssence();
        else if (chance <= 15 + addedChance) return GetARankEssence();
        else if (chance <= 30 + addedChance) return GetBRankEssence();
        else return GetCRankEssence();
    }

    public GameObject GetSSRankEssence()
    {
        return SSRankEssence[Random.Range(0, SSRankEssence.Count)];
    }

    public GameObject GetSRankEssence()
    {
        return SRankEssence[Random.Range(0, SRankEssence.Count)];
    }

    public GameObject GetARankEssence()
    {
        return ARankEssence[Random.Range(0, ARankEssence.Count)];
    }
    public GameObject GetBRankEssence()
    {
        return BRankEssence[Random.Range(0, BRankEssence.Count)];
    }
    public GameObject GetCRankEssence()
    {
        return CRankEssence[Random.Range(0, CRankEssence.Count)];
    }
}
