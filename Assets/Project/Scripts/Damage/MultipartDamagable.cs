using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultipartDamagable : Damagable
{

    [SerializeField] protected Segment segment;
    
    public override void Die()
    {
        if(damageAudioPlayer) damageAudioPlayer.PlayKilledSound();
        if(dispenser != null) dispenser.Dispense();
        segment.LoseSegment();
    }

}
