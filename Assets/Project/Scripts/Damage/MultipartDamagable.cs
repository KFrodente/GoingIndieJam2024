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

    public override bool IsPlayer => false;
    
    public override void Die(bool suicide = false)
    {
        if(damageAudioPlayer) damageAudioPlayer.PlayKilledSound();
        if(!suicide && dispenser != null) dispenser.Dispense();
        segment.LoseSegment();
    }

}

