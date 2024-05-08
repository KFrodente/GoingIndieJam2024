using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Possessable : MonoBehaviour
{
    [SerializeField] protected UnityEvent<SpiritSoul> OnStartPossess;
    [SerializeField] protected UnityEvent<Transform> OnEndPossess;
    protected SpiritSoul PossessingSpiritSoul;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Material canBePossessedMaterial;
    private Material defaultMaterial;
    

    public void Possess(SpiritSoul t)
    {
        OnStartPossess?.Invoke(t);
        PossessingSpiritSoul = t;
    }
    
    public void UnPossess(Transform t)
    {
        OnEndPossess?.Invoke(t);
        if (PossessingSpiritSoul != null)
        {
            PossessingSpiritSoul?.Reanimate(transform);
            
        }
    }
    private void Awake()
    {
        if(spriteRenderer) defaultMaterial = spriteRenderer.material;
    }

    public void ChangeToPossessSprite()
    {
        if(spriteRenderer) spriteRenderer.material = canBePossessedMaterial;
    }
    public void ChangeToUnpossessSprite()
    {
        if(spriteRenderer) spriteRenderer.material = defaultMaterial;
    }

}
