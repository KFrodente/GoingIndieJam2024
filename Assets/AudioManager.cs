using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    public bool doAudio = true;
    public static AudioManager instance;

    [SerializeField] private AudioClip pickUpSound;

    public void Play(AudioClip clip)
    {
        if(doAudio) source.PlayOneShot(clip);
    }
    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    public void PlayPickUpSound()
    {
        Play(pickUpSound);
    }
}
