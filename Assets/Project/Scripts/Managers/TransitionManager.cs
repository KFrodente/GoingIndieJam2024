using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    public Image blackScreen;

    private bool showBlack = false;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(instance);
        instance = this;
    }

    public IEnumerator FadeToBlack()
    {
        while (blackScreen.color.a < 1)
        {
            blackScreen.color = new Color(0, 0, 0, blackScreen.color.a + .1f);
            yield return new WaitForSeconds(.01f);
        }

    }

    public IEnumerator FadeOutOfBlack()
    {
        while (blackScreen.color.a > 0)
        {
            blackScreen.color = new Color(0, 0, 0, blackScreen.color.a - .1f);
            yield return new WaitForSeconds(.01f);
        }
        yield return null;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (showBlack)
    //    {
    //        blackScreen.transform.position = new Vector3(blackScreen.transform.position.x - 100, 0, 0);
    //    }
    //    if (!showBlack)
    //    {
    //        blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, blackScreen.color.a - .05f);
    //    }
    //}

    //public IEnumerator TransitionBetweenRooms(BaseCharacter character, Portal connectedPortal, Room currentRoom)
    //{
    //    showBlack = true;

    //    yield return new WaitUntil(() => blackScreen.transform.position.x >= 0);

    //    character.transform.position = connectedPortal.transform.position;

    //    if (currentRoom.TryGetComponent(out WalkerGenerator curRoom))
    //    {
    //        curRoom.SetRoomInactive();
    //    }


    //    if (connectedPortal.transform.parent.TryGetComponent(out WalkerGenerator nextRoom))
    //    {
    //        nextRoom.SetRoomActive();
    //    }

    //    showBlack = false;

    //    timer = 0;


    //    yield return null;
    //}
}
