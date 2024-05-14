using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoadManager : MonoBehaviour
{
    [SerializeField] Transform corvidViewpoint;
    [SerializeField] Transform dauntlessViewpoint;
    [SerializeField] Transform tetheredViewpoint;
    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        switch(CharacterSelectManager.selectedCharacter)
        {
            case CharacterSelectManager.Characters.Corvid:
                corvidViewpoint.transform.parent.parent.gameObject.SetActive(true);
                virtualCamera.Follow = corvidViewpoint;
                break;
            case CharacterSelectManager.Characters.Dauntless:
                dauntlessViewpoint.transform.parent.parent.gameObject.SetActive(true);
                virtualCamera.Follow = dauntlessViewpoint;
                break;
            case CharacterSelectManager.Characters.Tethered:
                tetheredViewpoint.transform.parent.parent.gameObject.SetActive(true);
                virtualCamera.Follow = tetheredViewpoint;
                break;


            default:
                //dauntlessViewpoint.transform.parent.parent.gameObject.SetActive(true);
                //virtualCamera.Follow = dauntlessViewpoint;
                break;
        }
    }
}
