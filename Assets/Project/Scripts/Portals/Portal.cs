using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : Interactable
{
    public Portal connectedPortal;
    public int costToEnter;
    public bool paidPrice = false;

    public PortalState state;

    [SerializeField] public SuperTextMesh portalText;

    private void Start()
    {
        state = new PortalUnOpened(this);
    }

    //Connect ALL portals after creating the portals

    public override void OnInteract(BaseCharacter character)
    {
        //checks if player is the tethered, if they are it checks if they are in their body
        if (CharacterSelectManager.selectedCharacter == CharacterSelectManager.Characters.Tethered && (!(character is TetheredCharacter))) return;

        base.OnInteract(character);

        Debug.Log("called enter portal");
        state.EnterPortal(character);

        //if (connectedPortal.transform.parent.TryGetComponent(out WalkerGenerator nextRoom))
        //{
        //    StartCoroutine(nextRoom.SetRoomActive(character, connectedPortal, GetComponentInParent<Room>()));
        //}
        //else
        //{
        //    StartCoroutine(Teleport(character));
        //}
    }

    public virtual IEnumerator Teleport(BaseCharacter character)
    {
        StartCoroutine(TransitionManager.instance.FadeToBlack());

        yield return new WaitUntil(() => TransitionManager.instance.blackScreen.color.a >= 1);

        character.transform.position = connectedPortal.transform.position;

        yield return new WaitForSecondsRealtime(.35f);

        StartCoroutine(TransitionManager.instance.FadeOutOfBlack());

        yield return null;
    }
}