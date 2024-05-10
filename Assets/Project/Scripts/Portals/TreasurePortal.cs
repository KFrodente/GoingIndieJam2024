using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePortal : Portal
{
    private void Start()
    {
        Vector2Int costToEnter = FloorGenerator.instance.floorStats[FloorGenerator.instance.floorNum].costToEnter;
        this.costToEnter = Random.Range(costToEnter.x, costToEnter.y);

    }

    public override void OnInteract(BaseCharacter character)
    {
        if (!paidPrice)
        {
            if (SpiritCharacter.souls - this.costToEnter >= 0)
            {
                SpiritCharacter.souls -= this.costToEnter;
                paidPrice = true;
                connectedPortal.GetComponentInParent<TreasureRoom>().GenerateSpiritEssence(this.costToEnter);

            }
        }

        else if(paidPrice)
        {
            Debug.Log($"Entered with {SpiritCharacter.souls} and paid price is: {paidPrice}");
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


        if (transform.parent.TryGetComponent(out WalkerGenerator room))
        {
            room.SetRoomInactive();
        }

        yield return null;
    }

}
