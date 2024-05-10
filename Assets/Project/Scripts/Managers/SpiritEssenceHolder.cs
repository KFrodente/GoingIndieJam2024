using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEssenceHolder : MonoBehaviour
{
    public static SpiritEssenceHolder instance;

    public List<GameObject> SSRankEssense = new();
    public List<GameObject> SRankEssense = new();
    public List<GameObject> ARankEssense = new();
    public List<GameObject> BRankEssense = new();
    public List<GameObject> CRankEssense = new();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }
}
