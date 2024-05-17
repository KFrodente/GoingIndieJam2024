using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPortal : Portal
{
    public override void OnInteract(BaseCharacter character)
    {
        base.OnInteract(character);

        //if done here it will happen after teleporting has finished
    }

    protected override IEnumerator Teleport(BaseCharacter character)
    {
        //if done here it will happen during the transition
        //feel free to change any of the code in here, the only "necessary" thing is the character transform setting :7) 

        StartCoroutine(TransitionManager.instance.FadeToBlack());

        yield return new WaitUntil(() => TransitionManager.instance.blackScreen.color.a >= 1);

        character.transform.position = connectedPortal.transform.position;

        yield return new WaitForSecondsRealtime(.35f);

        StartCoroutine(TransitionManager.instance.FadeOutOfBlack());

        yield return null;
    }
}
