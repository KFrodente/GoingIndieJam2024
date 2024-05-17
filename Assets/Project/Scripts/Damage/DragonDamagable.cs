using System.Collections;
using System.Collections.Generic;
using Stats;
using UnityEngine;

public class DragonDamagable : Damagable
{
    public int collectiveHealth;
    public override void Die(bool suicide = false)
    {
        
        StartCoroutine(TransitionManager.instance.FadeToBlack());
        StartCoroutine(TransitionManager.instance.SlideUpButton());
        TransitionManager.instance.TypeText2();
        CharacterSelectManager.selectedCharacter = CharacterSelectManager.Characters.None;
            
    }
    public new int Health
    {
        get { return health; }
        set
        {
            health = value;
            if(healthOverBar != null) healthOverBar.value = collectiveHealth / (float)startingHealth;
            Debug.Log("Correct health Stuff");
        }
    }

    public override bool TakeDamage(int damage, ProjectileDamageType type)
    {
        collectiveHealth -= damage;
        DoHealthStuff();
        //Debug.Log("TAKING DAMAGE: " + collectiveHealth + " | " + damage);
        
        return false;
    }

    private void DoHealthStuff()
    {
        if(healthOverBar != null) healthOverBar.value = collectiveHealth / (float)startingHealth;
    }
}
