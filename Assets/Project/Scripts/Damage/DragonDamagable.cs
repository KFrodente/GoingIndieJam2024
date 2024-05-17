using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class DragonDamagable : Damagable
{
    public override void Die(bool suicide = false)
    {
        
        StartCoroutine(TransitionManager.instance.FadeToBlack());
        StartCoroutine(TransitionManager.instance.SlideUpButton());
        TransitionManager.instance.TypeText2();
        CharacterSelectManager.selectedCharacter = CharacterSelectManager.Characters.None;
            
    }
}
