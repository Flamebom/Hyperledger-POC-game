using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }
    public static void Save(string saveString, string transactionString, string gemCountString)
    {
        File.WriteAllText(SAVE_FOLDER + "/save.txt", saveString);
        File.WriteAllText(SAVE_FOLDER + "/assets.txt", transactionString);
        File.WriteAllText(SAVE_FOLDER + "/game_money.txt", gemCountString);
    }
    public static string Load()
    {
        if (File.Exists(SAVE_FOLDER + "/save.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            return saveString;
        }
        else { return null; }
    }
    public static string LoadAssets() {
        if (File.Exists(SAVE_FOLDER + "/assets.txt"))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/assets.txt");
            return saveString;
        }
        else { return null; }
    }
}
