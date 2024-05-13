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

    public void OnCorvidClick()
    {
        selectedCharacter = Characters.Corvid;
    }
    public void OnTetheredClick()
    {
        selectedCharacter = Characters.Tethered;
    }
    public void OnDauntlessClick()
    {
        selectedCharacter = Characters.Dauntless;
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
}
