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
}
