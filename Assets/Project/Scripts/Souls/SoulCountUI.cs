using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCountUI : MonoBehaviour
{
    [SerializeField] SuperTextMesh text;
    public void UpdateSoulText(int newCount)
    {
        text.text = "<w=seasick>" + newCount;
    }

    private void Start()
    {
        text.text = "<w=seasick>" + 0;
    }
}
