using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private SuperTextMesh spiritControlText;
    [SerializeField] private SuperTextMesh text2;
    [SerializeField] private SuperTextMesh text3;
    [SerializeField] private float timeTillUnread;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spiritControlText.Read();
            StartCoroutine(unreadTimer());
        }

    }

    IEnumerator unreadTimer()
    {
        yield return new WaitForSeconds(timeTillUnread);
        spiritControlText.Unread();
        yield return new WaitForSeconds(0.6f);
        text2.Read();
        yield return new WaitForSeconds(timeTillUnread);
        text2.Unread();
        yield return new WaitForSeconds(0.6f);
        text3.Read();
        yield return new WaitForSeconds(timeTillUnread);
        text3.Unread();
    }
}
