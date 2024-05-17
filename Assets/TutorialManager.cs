using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private SuperTextMesh spiritControlText;
    [SerializeField] private SuperTextMesh text2;
    [SerializeField] private SuperTextMesh text3;
    [SerializeField] private SuperTextMesh text4;
    [SerializeField] private SuperTextMesh text5;
    [SerializeField] private float timeTillUnread;
    bool tutorialShown;

    private void Update()
    {
        if (!tutorialShown && Input.GetKeyDown(KeyCode.Q))
        {
            tutorialShown = true;
            spiritControlText.Read();
            StartCoroutine(unreadTimer());
        }

    }

    IEnumerator unreadTimer()
    {
        yield return new WaitForSeconds(timeTillUnread);
        spiritControlText.Unread();
        yield return new WaitForSeconds(1f);
        text2.Read();
        yield return new WaitForSeconds(timeTillUnread);
        text2.Unread();
        yield return new WaitForSeconds(1f);
        text3.Read();
        yield return new WaitForSeconds(timeTillUnread);
        text3.Unread();
        yield return new WaitForSeconds(1f);
        text4.Read();
        yield return new WaitForSeconds(timeTillUnread);
        text4.Unread();
        yield return new WaitForSeconds(1f);
        text5.Read();
        yield return new WaitForSeconds(timeTillUnread);
        text5.Unread();
    }
}
