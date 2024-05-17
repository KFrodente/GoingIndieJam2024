using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject characterSelectMenu;
    [SerializeField] private GameObject creditsScreen;

    private void Start()
    {
        SettingsManager sm = settingsMenu.GetComponent<SettingsManager>();
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            sm.SetVolume(settingsData.volume);
            sm.SetMusicVolume(settingsData.musicVolume);
        }
    }

    public void ShowSettings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ExitSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ExitCharacterSelect()
    {
        characterSelectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        characterSelectMenu.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();

        //this is so that it doesn't get mad when you build the game
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #endif
    }

    public void OnCredits()
    {
        mainMenu.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void ExitCredits()
    {
        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);
    }
}
