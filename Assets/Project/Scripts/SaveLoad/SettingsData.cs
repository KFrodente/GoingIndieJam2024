using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public float volume;
    public float musicVolume;

    public SettingsData(SettingsManager settings)
    {
        volume = settings.volume;
        musicVolume = settings.musicVolume;
    }

    public SettingsData() { }
}
