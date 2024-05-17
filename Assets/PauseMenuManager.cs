using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    public GameObject settingsUI;
    private bool paused = false;

    private void Start()
    {
        SettingsManager sm = settingsUI.GetComponent<SettingsManager>();
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            sm.SetVolume(settingsData.volume);
            sm.SetMusicVolume(settingsData.musicVolume);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                if (settingsUI.activeSelf)
                {
                    //pauseUI.SetActive(true);
                    settingsUI.SetActive(false);
                }
                else if (pauseScreen.activeSelf)
                {
                    Resume();
                }
            }
            else
            {
                Pause();
            }
        }
    }

    public void OnResume()
    {
        Resume();
    }

    public void OnSettings()
    {
        settingsUI.SetActive(true);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1;
        paused = false;
        AudioManager.instance.doAudio = true;
        SceneManager.LoadScene("MainMenu");
    }

    private void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    private void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void OnBack()
    {
        pauseScreen.SetActive(true);
        settingsUI.SetActive(false);
    }
}
