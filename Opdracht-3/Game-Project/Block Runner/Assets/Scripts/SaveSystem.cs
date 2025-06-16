using System.IO;
using UnityEngine;


public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.persistentDataPath + "/saves/";
    public static readonly string FILE_EXT = ".json";


    public static void Save(string fileName, string dataToSave)
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER); // Create the save folder if it doesn't exist
        }

        string filePath = SAVE_FOLDER + fileName + FILE_EXT;
        File.WriteAllText(SAVE_FOLDER + fileName + FILE_EXT, dataToSave);
    }

    public static string Load(string fileName)
    {
        string fileLoc = SAVE_FOLDER + fileName + FILE_EXT;
        if (File.Exists(fileLoc))
        {
            string loadedData = File.ReadAllText(fileLoc);
            return loadedData;
        }
        else
        {
            return null;
        }
    }
}
// this script handles saving data to a file in Unity, specifically for saving game state or settings.