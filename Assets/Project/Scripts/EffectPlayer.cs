using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    public virtual void Play()
    {
        
    }
    protected virtual void Quit()
    {
        
    }
}
public class AfterImageEffect : EffectPlayer
{
    [SerializeField] protected SpriteRenderer baseImage;
    [SerializeField] protected float activeTime = 0.1f;
    [SerializeField] protected float alphaSet = 0.8f;
    [SerializeField] protected float alphaMultipler = 0.85f;
    [SerializeField] protected SpriteRenderer afterImage;
    protected float timeActivated;
    protected float alpha;
    
    public override void Play()
    {
        timeActivated = Time.time;
        alpha = alphaSet;
        afterImage.enabled = true;
    }

    protected virtual void Update()
    {
        alpha *= alphaMultipler;
        afterImage.color = new Color(1f, 1f, 1f, alpha);
        if (Time.time >= (timeActivated + activeTime))
        {
            Quit();
        }
        
    }

    protected override void Quit()
    {
        afterImage.enabled = false;
    }
}
