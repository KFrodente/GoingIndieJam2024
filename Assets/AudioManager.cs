using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public static AudioManager instance;

    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
