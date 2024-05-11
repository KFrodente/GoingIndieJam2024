using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCountUI : MonoBehaviour
{
    [SerializeField] SuperTextMesh text;
    public void UpdateSoulText(int newCount)
    {
        text.text = newCount.ToString();
    }

    private void Start()
    {
        text.text = 0.ToString();
    }
}
