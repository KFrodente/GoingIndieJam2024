using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    public enum Characters
    {
        None,
        Corvid,
        Tethered,
        Dauntless
    }
    public static Characters selectedCharacter = Characters.None;

    [SerializeField] private GameObject warningText;

    public SuperTextMesh corvidText;
    public SuperTextMesh tetheredText;
    public SuperTextMesh dauntlessText;

    public void OnCorvidClick()
    {
        if(selectedCharacter == Characters.Tethered) tetheredText.Unread();
        else if (selectedCharacter == Characters.Dauntless) dauntlessText.Unread();
        selectedCharacter = Characters.Corvid;
        corvidText.Read();

    }
    public void OnTetheredClick()
    {
        if (selectedCharacter == Characters.Dauntless) dauntlessText.Unread();
        else if (selectedCharacter == Characters.Corvid) corvidText.Unread();
        selectedCharacter = Characters.Tethered;
        tetheredText.Read();
    }
    public void OnDauntlessClick()
    {
        if (selectedCharacter == Characters.Corvid) corvidText.Unread();
        else if (selectedCharacter == Characters.Tethered) tetheredText.Unread();
        selectedCharacter = Characters.Dauntless;
        dauntlessText.Read();
    }

    public void OnPlay()
    {
        if(selectedCharacter == Characters.None)
        { // tell player to pick a character
            warningText.SetActive(true);
        }
        else
        { // Start game
            SceneManager.LoadScene("Game");
        }
    }

    public void OnTutorial()
    {
        switch(selectedCharacter)
        {
            case Characters.None:
                warningText.SetActive(true);
                break;
            case Characters.Corvid:
                SceneManager.LoadScene("CorvidTutorial");
                break;
            case Characters.Tethered:
                SceneManager.LoadScene("TetheredTutorial");
                break;
            case Characters.Dauntless:
                SceneManager.LoadScene("DauntlessTutorial");
                break;
        }
    }
}
