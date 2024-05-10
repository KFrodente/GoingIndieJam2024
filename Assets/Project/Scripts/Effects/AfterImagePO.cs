using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImagePO : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer afterImage;
    [SerializeField, Header("Data")] protected float activeTime = 0.1f;
    [SerializeField] protected float startAlpha = 0f;
    [SerializeField] protected float endAlpha = 0.8f;

    protected Timer timer;
    protected AfterImageEffect objectPool;
    public void Init(AfterImageEffect pool)
    {
        Reset(pool);
        timer = new CountdownTimer(activeTime);
        timer.OnTimerStop += () => Disable();
        timer.Start();
    }

    protected void Reset(AfterImageEffect pool)
    {
        objectPool = pool;
        transform.position = pool.transform.position;
        gameObject.SetActive(true);
        afterImage.color = new Color(1f, 1f, 1f, startAlpha);
    }

    protected void Update()
    {
        if (timer == null) return;
        timer.Tick(Time.deltaTime);
        afterImage.color = new Color(1f, 1f, 1f, Mathf.Lerp(startAlpha, endAlpha, timer.Progress));
    }

    protected void Disable()
    {
        gameObject.SetActive(false);
        objectPool.Enqueue(this);
    }
}
