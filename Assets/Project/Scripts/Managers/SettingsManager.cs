using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsManager : MonoBehaviour
{
    public GameObject titleUI;
    public GameObject buttonsUI;


    public AudioMixer mixer;
    public AudioMixer musicMixer;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    private Resolution[] _resolutions;
    public Slider volumeSlider;
    public Slider musicVolumeSlider;

    private bool settingsJustOpened = false;

    // Settings data
    public float volume;
    public float musicVolume;

    //public PauseMenu pauseMenu;

    private void OnEnable()
    {
        SettingsData settingsData = SaveSystem.LoadSettings();
        if (settingsData != null)
        {
            if (volumeSlider != null) volumeSlider.value = settingsData.volume;
            if (musicVolumeSlider != null) musicVolumeSlider.value = settingsData.musicVolume;
        }

        fullscreenToggle.isOn = Screen.fullScreen;

        int currentResIndex = 0;
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> stringRes = new List<string>();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            stringRes.Add($"{_resolutions[i].width} X {_resolutions[i].height} {(int)(_resolutions[i].refreshRateRatio.value)}hz");
            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(stringRes);
        //resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetRes(int index)
    {
        if (!settingsJustOpened)
        {
            Resolution resolution = _resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
    //public void OnBack()
    //{
    //    if (buttonsUI != null) buttonsUI.SetActive(true);
    //    if (!GameStarted.started)
    //    {
    //        if (titleUI != null) titleUI.SetActive(true);

    //    }
    //    else
    //    {
    //        pauseMenu.buttonAnimator.SetBool("Paused", true);
    //        pauseMenu.pauseText.SetBool("Showing", true);
    //    }
    //    gameObject.SetActive(false);
    //}

    public void SetVolume(float value)
    {
        volume = value;
        mixer.SetFloat("Volume", value);
        SaveSystem.SaveSettings(this);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicMixer.SetFloat("Volume", value);
        SaveSystem.SaveSettings(this);
    }

    public void SetFullScreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
}
