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
    [SerializeField] private SuperTextMesh text6;
    [SerializeField] private SuperTextMesh text7;
    [SerializeField] private SuperTextMesh text8;
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

        if(text6 != null)
        {
            yield return new WaitForSeconds(1f);
            text6.Read();
            yield return new WaitForSeconds(timeTillUnread);
            text6.Unread();

        }

        if (text7 != null)
        {
            yield return new WaitForSeconds(1f);
            text7.Read();
            yield return new WaitForSeconds(timeTillUnread);
            text7.Unread();
        }

        if (text8 != null)
        {
            yield return new WaitForSeconds(1f);
            text8.Read();
            yield return new WaitForSeconds(timeTillUnread);
            text8.Unread();
        }
    }
}
