using System.Collections.Generic;
using UnityEngine;

public class AfterImageEffect : EffectPlayer
{
    [SerializeField] protected AfterImagePO baseAfterImage;
    [SerializeField] protected float spacing = 0.05f;
    protected Queue<AfterImagePO> objectPool = new Queue<AfterImagePO>();
    

    public void Enqueue(AfterImagePO po)
    {
        objectPool.Enqueue(po);
    }

    protected Vector2 lastPos;
    protected float endTime;
    public override void Play(Vector2 position, float duration)
    {
        endTime = Time.time + duration;
        SpawnAfterEffect();
    }

    protected AfterImagePO GetPoolObject()
    {
        if (objectPool.Count == 0)
        {
            return GrowPool();
        }

        return objectPool.Dequeue();
    }

    protected AfterImagePO GrowPool()
    {
        return Instantiate(baseAfterImage, transform.position, transform.rotation).GetComponent<AfterImagePO>();
    }

    protected virtual void Update()
    {
        if (Time.time < endTime && Vector2.Distance(lastPos , transform.position) > spacing)
        {
            SpawnAfterEffect();
        }
    }

    protected void SpawnAfterEffect()
    {
        AfterImagePO afterImage = GetPoolObject();
        afterImage.Init(this);
        lastPos = transform.position;
    }
    
}