using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static GenealogyDefine;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [SerializeField] private float defaultSound = 0.5f;
    [SerializeField] private PlayerData player;
    [SerializeField] private SynergyInfoDataSO _synergyInfoDataSO;

    public PlayerData CurrentPlayer { get { return player; } }

    private string SAVE_PATH = "";
    private const string SAVE_FILE = "/SaveFile.Json";

    private void Awake()
    {
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

    public void SetGenealogyData(GenealogyData data)
    {
        if (player.genealogySaveCnt >= 5) return;

        player.genealogyDatas[player.genealogySaveCnt++] = data;

        player.SetPlayerSynergy(data);
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

    private void InitSyneargyInfoDict()
    {
        int[] datas;


    }

    //private IEnumerator SyneargyDataDownLoad()
    //{

    //    const string URL = "https://docs.google.com/spreadsheets/d/1xcHXDI1P4fY2vODt-s3mA51y1kgPkx9c-_dPHs7LYKw/export?format=tsv&gid=0&range=C2:12";
    //    UnityWebRequest www = UnityWebRequest.Get(URL);
    //    yield return www.SendWebRequest();

    //    SetSyneargyData(www.downloadHandler.text);
    //}

    //private void SetMonsterDatas(string data)
    //{
    //    string[] row = data.Split('\n');
    //    string[] column;
    //    int rowSize = row.Length;
    //    int colummSize = row[0].Split('\t').Length;
    //    MonsterBase monsterBase = null;

    //    string id = "";
    //    string name = "";
    //    string itemId = "";
    //    PropertyType type;
    //    ItemBase item = null;

    //    for (int i = 0; i < rowSize; i++)
    //    {
    //        column = row[i].Split('\t');
    //        for (int j = 0; j < colummSize; j++)
    //        {
    //            id = column[0];
    //            name = column[1];
    //            type = (PropertyType)Enum.Parse(typeof(PropertyType), column[2]);
    //            column[3] = Regex.Replace(column[3], "[^a-zA-Z_]", "");
    //            item = FindItemBase(column[3]);

    //            if (i >= monsterDatas.monsterDatas.Count)
    //            {

    //                monsterDatas.monsterDatas.Add(new MonsterBase(id, name, type, item));
    //            }

    //            else
    //            {
    //                monsterBase = monsterDatas.monsterDatas[i];
    //                monsterBase.monsterId = id;
    //                monsterBase.monsterName = name;
    //                monsterBase.monsterType = type;
    //                monsterBase.dropItem = item;
    //            }
    //        }
    //    }
    //}

    private void AddSynergyInfo(ESynergy type, int[] datas)
{

}

}