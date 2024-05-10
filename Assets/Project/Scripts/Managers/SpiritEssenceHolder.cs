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
