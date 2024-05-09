using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageSprite : MonoBehaviour
{
    [SerializeField] private float activeTime = 0.1f;
    [SerializeField] private float alphaSet = 0.8f;
    [SerializeField] private float alphaMultipler = 0.85f;
    private float timeActivated;
    private float alpha;


    private Transform GFXtarget;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer targetSpriteRenderer;

    private Color color;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GFXtarget = GameObject.FindGameObjectWithTag("CorvidGFX").transform;
        targetSpriteRenderer = GFXtarget.GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = targetSpriteRenderer.sprite;
        alpha = alphaSet;
        transform.position = GFXtarget.position;
        transform.rotation = GFXtarget.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultipler;
        color = new Color(1f, 1f, 1f, alpha);
        spriteRenderer.color = color;

        if (Time.time >= (timeActivated + activeTime))
        {
            AfterImagePool.Instance.AddToPool(gameObject);
        }
    }


}
