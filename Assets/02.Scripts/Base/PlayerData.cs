using System.Collections.Generic;
using UnityEngine;
using static GenealogyDefine;

public struct PlayerStat
{
    public float health;
    public float atkPower;
    public float atkSpeed;
    public float moveSpeed;
    public float defense;
    public float criticalPercent;
    public float criticalPower;
    public float decreaseCooldown;
}

[System.Serializable]
public class PlayerData
{
    public float effectSoundVolume;
    public float bgmSoundVolume;

    private Dictionary<ESynergy, List<GenealogyData>> syneargyDict;

    public bool isTutorial;

    public PlayerStat playerStats;
    public GenealogyData[] genealogyDatas;
    public int genealogySaveCnt;

    public PlayerData(float soundVolume)
    {
        effectSoundVolume = soundVolume;
        bgmSoundVolume = soundVolume;
        isTutorial = false;
        syneargyDict = new Dictionary<ESynergy, List<GenealogyData>>();
        genealogyDatas = new GenealogyData[5];

        playerStats = new PlayerStat();
    }

    public void SetPlayerSynergy(GenealogyData data, bool isAdd = true)
    {
        EGenealogy type = data.genealogyType;

        if (type == EGenealogy.Rest)
        {
            SetSynergy(ESynergy.Rest, data, isAdd);
        }

        if (type == EGenealogy.Pair)
        {
            SetSynergy(ESynergy.Pair, data, isAdd);
        }

        if (type == EGenealogy.LightPair)
        {
            SetSynergy(ESynergy.LightPair, data, isAdd);
        }

        if (type == EGenealogy.SeRyuk ||
            type == EGenealogy.Jangsa ||
            type == EGenealogy.BBing ||
            type == EGenealogy.Doksa ||
            type == EGenealogy.Ali)
        {
            SetSynergy(ESynergy.Middle, data, isAdd);
        }

        if (type == EGenealogy.ESibal ||
            type == EGenealogy.PairHunter ||
            type == EGenealogy.GuSa ||
            type == EGenealogy.GabO)
        {
            SetSynergy(ESynergy.Special, data, isAdd);
        }

        if (type == EGenealogy.SeRyuk ||
           (type == EGenealogy.BBing && // ±¸»æ
            data.genealogyNum == 9) || // ±¸»æ
            type == EGenealogy.PairHunter)
        {
            SetSynergy(ESynergy.Mangtong, data, isAdd);
        }
    }

    private void SetSynergy(ESynergy type, GenealogyData data, bool isAdd)
    {
        if (syneargyDict.ContainsKey(type))
        {
            if (isAdd)
            {
                syneargyDict[type].Add(data);
            }

            else
            {
                syneargyDict[type].Remove(data);
            }

            SetStatFromSynergy(type);
        }
    }

    private void SetStatFromSynergy(ESynergy type)
    {
        switch (type)
        {
            case ESynergy.Rest:
            {

                float atkPower = 0f;
                foreach (GenealogyData data in syneargyDict[type])
                {
                    atkPower += data.genealogyNum;
                }

                atkPower -=  (atkPower % 10);

                atkPower *= syneargyDict[type].Count;

                playerStats.atkPower += atkPower;
                break;
            }

            case ESynergy.Pair:
            {
                int percent = 10 * syneargyDict[type].Count;
                float atkPower = UtilDefine.CalcPercent(playerStats.atkPower, percent);
                playerStats.atkPower += atkPower;

                break;
            }

        }
    }
}
