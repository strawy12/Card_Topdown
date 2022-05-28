using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using static GenealogyDefine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

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
        //if (File.Exists(SAVE_PATH + SAVE_FILE))
        //{
        //    string stringJson = File.ReadAllText(SAVE_PATH + SAVE_FILE);
        //    player = JsonUtility.FromJson<PlayerData>(stringJson);
        //}
       // else
       // {
            player = new PlayerData(defaultSound);
       // }
       // SaveToJson();
       // SaveToJson();
    }

    public void SetGenealogyData(GenealogyData data)
    {
        if (player.GenealogyCnt >= 5) return;

        player.genealogyDatas[player.GenealogyCnt] = data;
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

            synergyInfo = new SynergyInfo();

            maxLevel = int.Parse(column[0]);
            count = int.Parse(column[1]);

            for (int j = 0; j < count; j++)
            {
                synergyInfo.Add(SetSynergyInfoList(column, j, maxLevel));
            }

            synergyInfo.type = (ESynergy)rowCnt;

            _synergyInfoDataSO[rowCnt] = synergyInfo;

            rowCnt++;
        }
    }

    private SynergyDataList SetSynergyInfoList(string[] column, int cnt, int maxLevel)
    {
        SynergyDataList dataList = new SynergyDataList();

        for (int i = 0; i < maxLevel; i++)
        {
            // 여기의 로직을 변경해야됨
            int dataIndex = 2 + (cnt * maxLevel) + i;
            if (dataIndex >= column.Length) continue;

            column[dataIndex] = Regex.Replace(column[dataIndex], @"\D", "");

            if (column[dataIndex] == "") continue;

            dataList.dataList.Add(int.Parse(column[dataIndex]));
            dataIndex++;
        }

        return dataList;
    }

    public int GetSynergyInfoData(ESynergy type, int idx, int level)
    {
        return _synergyInfoDataSO[type][idx][level - 1];
    }

    private void OnDestroy()
    {
      //  SaveToJson();
    }

    private void OnApplicationQuit()
    {
     //   SaveToJson();
    }

}