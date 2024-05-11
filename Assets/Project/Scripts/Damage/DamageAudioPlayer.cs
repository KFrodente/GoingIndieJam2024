using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip killedSound;

    public void PlayHitSound()
    {
        // Mach variables : pitch | volume
        AudioManager am = AudioManager.instance;
        if(am != null) am.Play(hitSound);
    }
    public void PlayKilledSound()
    {
        // Mach variables : pitch | volume
        AudioManager am = AudioManager.instance;
        if(am != null) am.Play(killedSound);
    }
}
