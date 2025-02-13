using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePortal : Portal
{


    private void Start()
    {
        Vector2Int costToEnter = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum].costToEnter;
        this.costToEnter = Random.Range(costToEnter.x, costToEnter.y);
        portalText.text = "<w=seasick>" + this.costToEnter.ToString() + " souls";
    }

    public override void OnInteract(BaseCharacter character)
    {
        if (CharacterSelectManager.selectedCharacter == CharacterSelectManager.Characters.Tethered && (!(character is TetheredCharacter))) return;

        SpiritCharacter spirit = character.possessingSpirit;
        if (!paidPrice)
        {
            if (spirit.Souls - this.costToEnter >= 0)
            {
                portalText.text = "<w=seasick>Not Entered";
                spirit.Souls -= this.costToEnter;
                paidPrice = true;
                connectedPortal.GetComponentInParent<TreasureRoom>().GenerateSpiritEssence(this.costToEnter);

            }
        }

        else if(paidPrice)
        {
            Debug.Log($"Entered with {spirit.Souls} and paid price is: {paidPrice}");

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
