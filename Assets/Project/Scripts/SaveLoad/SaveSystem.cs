using UnityEngine;
using System.IO; // namespace for working with files
using System.Runtime.Serialization.Formatters.Binary;

//using UnityEditor.Experimental.RestService; // Binary formatter access

public static class SaveSystem
{
    // Gets a path to a data directory on the operating system that wont change
    // This is universal to different OS
    private static string settingsDataPath = Application.persistentDataPath + "/settings.save";

    public static void SaveSettings(SettingsManager settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(settingsDataPath, FileMode.Create);
        SettingsData data = new SettingsData(settings);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SettingsData LoadSettings()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(settingsDataPath))
        {
            FileStream stream = new FileStream(settingsDataPath, FileMode.Open);

            SettingsData data = formatter.Deserialize(stream) as SettingsData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Settings save file not found in " + settingsDataPath + ", creating save");
            FileStream stream = new FileStream(settingsDataPath, FileMode.Create);

            SettingsData data = new SettingsData();
            formatter.Serialize(stream, data);

            stream.Close();
            return null;
        }
    }


    #region EXAMPLE SAVE-LOAD FUNCTIONS
    //public static void SavePlayer(DataTestPlayer player)
    //{
    //    BinaryFormatter formatter = new BinaryFormatter();

    //    FileStream stream = new FileStream(playerDataPath, FileMode.Create); // file created at persistentDataPath/player.skrimp

    //    PlayerData data = new PlayerData(player);

    //    formatter.Serialize(stream, data);

    //    stream.Close(); // always close stream
    //}

    //public static PlayerData LoadPlayer()
    //{
    //    if(File.Exists(playerDataPath))
    //    {
    //        BinaryFormatter formatter = new BinaryFormatter();
    //        FileStream stream = new FileStream(playerDataPath, FileMode.Open);

    //        PlayerData data = formatter.Deserialize(stream) as PlayerData;
    //        stream.Close();

    //        return data;
    //    }
    //    else
    //    {
    //        Debug.LogError("Player save file not found in " + playerDataPath);
    //        return null;
    //    }
    //}
    #endregion
}
