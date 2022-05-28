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
public class SyneargyElement
{
    public ESynergy _syneargyType;
    public List<GenealogyData> _genealogylist;

    public int Level { get => _genealogylist.Count; }

    public GenealogyData this[int idx]
    {
        get => _genealogylist[idx];
    }


    public void Add(GenealogyData data)
    {
        if (data == null) return;

        _genealogylist.Add(data);
    }

    public void Remove(GenealogyData data)
    {
        if (data == null) return;

        _genealogylist.Remove(data);
    }

}

[System.Serializable]
public class PlayerData
{
    public float effectSoundVolume;
    public float bgmSoundVolume;

    [SerializeField] private List<SyneargyElement> syneargyDict;

    public bool isTutorial;

    public PlayerStat playerStats;
    public GenealogyData[] genealogyDatas;
    public int saveCardCnt;
    public int GenealogyCnt
    {
        get => saveCardCnt / 2;
    }

    public bool beingAddedCard;

    public PlayerData(float soundVolume)
    {
        effectSoundVolume = soundVolume;
        bgmSoundVolume = soundVolume;
        isTutorial = false;
        syneargyDict = new List<SyneargyElement>();
        genealogyDatas = new GenealogyData[5];
        saveCardCnt = 0;
        playerStats = new PlayerStat();
        InitSyneargyDict();
    }

    private void InitSyneargyDict()
    {
        for (ESynergy type = ESynergy.Rest; type < ESynergy.Count; type++)
        {
            SyneargyElement element = new SyneargyElement();
            element._syneargyType = type;
            element._genealogylist = new List<GenealogyData>();

            syneargyDict.Add(element);
        }
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
        {
            if (isAdd)
            {
;                syneargyDict[(int)type].Add(data);
            }

            else
            {
                syneargyDict[(int)type].Remove(data);
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
                    int restSum = 0;
                    foreach (GenealogyData data in syneargyDict[(int)ESynergy.Rest]._genealogylist)
                    {
                        restSum += data.genealogyNum;
                    }
                    restSum -= (restSum % 10);

                    int level = syneargyDict[(int)ESynergy.Rest].Level;
                    
                    playerStats.atkPower += restSum * GameManager.Inst.Data.GetSynergyInfoData(ESynergy.Rest, 0, level);
                    break;
                }

            case ESynergy.Pair:
                {
                    int level = syneargyDict[(int)ESynergy.Pair].Level;
                    int percent = GameManager.Inst.Data.GetSynergyInfoData(ESynergy.Pair, 0, level);
                    playerStats.atkPower += UtilDefine.CalcPercent(playerStats.atkPower, percent) * level;

                    percent = GameManager.Inst.Data.GetSynergyInfoData(ESynergy.Pair, 1, level);
                    playerStats.atkSpeed += UtilDefine.CalcPercent(playerStats.atkSpeed, percent) * level * GenealogyCnt;
                    break;
                }

            case ESynergy.LightPair:
                {
                    int numSum = syneargyDict[(int)ESynergy.LightPair][0].genealogyNum;
                    int level = -1;
                    switch (numSum)
                    {
                        case 4:
                            level = 0;
                            break;

                        case 9:
                            level = 1;
                            break;

                        case 11:
                            level = 2;
                            break;
                    }

                    if (level == -1) return;

                    playerStats.atkPower += UtilDefine.CalcPercent(playerStats.atkPower,
                                                        (numSum * (5 + GenealogyCnt)));
                    playerStats.criticalPercent += UtilDefine.CalcPercent(playerStats.criticalPercent,
                                                        (15 * (5 + saveCardCnt)));
                    break;
                }

        }
    }

}
