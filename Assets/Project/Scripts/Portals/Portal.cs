using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : Interactable
{
    public Portal connectedPortal;
    public int costToEnter;
    public bool paidPrice = false;

    [SerializeField] public SuperTextMesh portalText;

    public bool active;

    //Connect ALL portals after creating the portals

    

    public override void OnInteract(BaseCharacter character)
    {
        if (CharacterSelectManager.selectedCharacter == CharacterSelectManager.Characters.Tethered && (!(character is TetheredCharacter))) return;

        base.OnInteract(character);

        portalText.text = "";
        connectedPortal.portalText.text = "";
        if (connectedPortal.transform.parent.TryGetComponent(out WalkerGenerator nextRoom))
        {
            StartCoroutine(nextRoom.SetRoomActive(character, connectedPortal, GetComponentInParent<Room>()));
        }
        else
        {
            StartCoroutine(Teleport(character));
        }
    }

    private IEnumerator Teleport(BaseCharacter character)
    {
        StartCoroutine(TransitionManager.instance.FadeToBlack());

        yield return new WaitUntil(() => TransitionManager.instance.blackScreen.color.a >= 1);

        character.transform.position = connectedPortal.transform.position;

        yield return new WaitForSecondsRealtime(.35f);

        StartCoroutine(TransitionManager.instance.FadeOutOfBlack());

        yield return null;
    }
}
