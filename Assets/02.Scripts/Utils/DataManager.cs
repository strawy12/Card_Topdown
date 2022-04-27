using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DataManager : MonoSingleton<DataManager>
{
    [SerializeField] private float defaultSound = 0.5f;
    [SerializeField] private PlayerData player;

    public PlayerData CurrentPlayer { get { return player; } }

    private string SAVE_PATH = "";
    private const string SAVE_FILE = "/SaveFile.Json";

    private void Awake()
    {
        DataManager[] dmanagers = FindObjectsOfType<DataManager>();
        if (dmanagers.Length != 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }

        LoadFromJson();
        //SoundVolumeUpdate();
    }

    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILE))
        {
            string stringJson = File.ReadAllText(SAVE_PATH + SAVE_FILE);
            player = JsonUtility.FromJson<PlayerData>(stringJson);
        }
        else
        {
            player = new PlayerData(defaultSound);
        }
        SaveToJson();
    }
    public void SaveToJson()
    {
        string stringJson = JsonUtility.ToJson(player, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILE, stringJson, System.Text.Encoding.UTF8);
    }
    public void DataReset()
    {
        player = new PlayerData(defaultSound);
        SaveToJson();
        Application.Quit();
    }

}   