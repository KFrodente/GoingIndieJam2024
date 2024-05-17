using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GraphicsRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> graphics = new List<Sprite>();
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer.sprite = graphics[Random.Range(0, graphics.Count)];
    }
}