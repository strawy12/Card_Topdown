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
        StartCoroutine(SyneargyDataDownLoad());
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


    private IEnumerator SyneargyDataDownLoad()
    {

        const string URL = "https://docs.google.com/spreadsheets/d/1xcHXDI1P4fY2vODt-s3mA51y1kgPkx9c-_dPHs7LYKw/export?format=tsv&gid=0&range=C2:12";
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        SetSyneargyData(www.downloadHandler.text);
    }

    private void SetSyneargyData(string data)
    {
        Debug.Log(data);
        string[] row = data.Split('\n');
        string[] column;
        int rowSize = row.Length;

        int rowCnt = 0;

        SynergyInfo synergyInfo;

        int maxLevel;
        int count;

        for (int i = 0; i < rowSize; i++)
        {
            column = row[i].Split('\t');
            if (column[0] == "") continue;
            Debug.Log(column[0]);
            synergyInfo = new SynergyInfo();

            maxLevel = int.Parse(column[0]);
            count = int.Parse(column[1]);

            for (int j = 0; j < count; j++)
            {
                synergyInfo.Add(SetSynergyInfoList(column, maxLevel));
            }

            synergyInfo.type = (ESynergy)rowCnt;

            _synergyInfoDataSO[rowCnt] = synergyInfo;

            rowCnt++;
        }
    }

    private SynergyDataList SetSynergyInfoList(string[] column, int cnt)
    {
        int idx = 0;
        SynergyDataList dataList = new SynergyDataList();

        for (int i = 0; i < cnt; i++)
        {
            if (column[2 + idx] == "") continue;

            dataList.dataList.Add(int.Parse(column[2 + idx]));
            idx++;
        }

        return dataList;
    }

    private void AddSynergyInfo(ESynergy type, int[] datas)
    {

    }

}