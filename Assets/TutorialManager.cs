using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private SuperTextMesh spiritControlText;
    [SerializeField] private float timeTillUnread;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spiritControlText.Read();
        }

    }

    IEnumerator unreadTimer()
    {
        yield return new WaitForSeconds(timeTillUnread);
        spiritControlText.Unread();
    }
}
