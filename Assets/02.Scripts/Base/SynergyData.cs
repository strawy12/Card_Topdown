//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SynergyData : MonoBehaviour
//{
//    public class SyneargyData
//    {
//        private Dictionary<ESynergy, List<GenealogyData>> synergyDict;
//        public GenealogyData[] genealogyDatas;
//        public bool beingAddedCard;
//        public int saveCardCnt;
//        public int GenealogyCnt
//        {
//            get => saveCardCnt / 2;
//        }
//        private void InitSyneargyDict()
//        {
//            for (ESynergy type = ESynergy.Rest; type < ESynergy.Count; type++)
//            {
//                SyneargyElement element = new SyneargyElement();
//                element._syneargyType = type;
//                element._genealogylist = new List<GenealogyData>();

//                syneargyDict.Add(element);
//            }
//        }

//        public void SetPlayerSynergy(GenealogyData data, bool isAdd = true)
//        {
//            EGenealogy type = data.genealogyType;

//            if (type == EGenealogy.Rest)
//            {
//                SetSynergy(ESynergy.Rest, data, isAdd);
//            }

//            if (type == EGenealogy.Pair)
//            {
//                SetSynergy(ESynergy.Pair, data, isAdd);
//            }

//            if (type == EGenealogy.LightPair)
//            {
//                SetSynergy(ESynergy.LightPair, data, isAdd);
//            }

//            if (type == EGenealogy.SeRyuk ||
//                type == EGenealogy.Jangsa ||
//                type == EGenealogy.BBing ||
//                type == EGenealogy.Doksa ||
//                type == EGenealogy.Ali)
//            {
//                SetSynergy(ESynergy.Middle, data, isAdd);
//            }

//            if (type == EGenealogy.ESibal ||
//                type == EGenealogy.PairHunter ||
//                type == EGenealogy.GuSa ||
//                type == EGenealogy.GabO)
//            {
//                SetSynergy(ESynergy.Special, data, isAdd);
//            }

//            if (type == EGenealogy.SeRyuk ||
//               (type == EGenealogy.BBing && // ±¸»æ
//                data.genealogyNum == 9) || // ±¸»æ
//                type == EGenealogy.PairHunter)
//            {
//                SetSynergy(ESynergy.Mangtong, data, isAdd);
//            }
//        }

//        private void SetSynergy(ESynergy type, GenealogyData data, bool isAdd)
//        {
//            {
//                if (isAdd)
//                {
//                    ; syneargyDict[(int)type].Add(data);
//                }

//                else
//                {
//                    syneargyDict[(int)type].Remove(data);
//                }

//                SetStatFromSynergy(type);
//            }
//        }

//        private void SetStatFromSynergy(ESynergy type)
//        {
//            switch (type)
//            {
//                case ESynergy.Rest:
//                    {
//                        int restSum = 0;
//                        foreach (GenealogyData data in syneargyDict[(int)ESynergy.Rest]._genealogylist)
//                        {
//                            restSum += data.genealogyNum;
//                        }
//                        restSum -= (restSum % 10);

//                        int level = syneargyDict[(int)ESynergy.Rest].Level;

//                        playerStats.attackDamage += restSum * GameManager.Inst.Data.GetSynergyInfoData(ESynergy.Rest, 0, level);
//                        break;
//                    }

//                case ESynergy.Pair:
//                    {
//                        int level = syneargyDict[(int)ESynergy.Pair].Level;
//                        int percent = GameManager.Inst.Data.GetSynergyInfoData(ESynergy.Pair, 0, level);
//                        playerStats.attackDamage += UtilDefine.CalcPercent(playerStats.attackDamage, percent) * level;

//                        percent = GameManager.Inst.Data.GetSynergyInfoData(ESynergy.Pair, 1, level);
//                        playerStats.attackSpeed += UtilDefine.CalcPercent(playerStats.attackSpeed, percent) * level * GenealogyCnt;
//                        break;
//                    }

//                case ESynergy.LightPair:
//                    {
//                        int numSum = syneargyDict[(int)ESynergy.LightPair][0].genealogyNum;
//                        int level = -1;
//                        switch (numSum)
//                        {
//                            case 4:
//                                level = 0;
//                                break;

//                            case 9:
//                                level = 1;
//                                break;

//                            case 11:
//                                level = 2;
//                                break;
//                        }

//                        if (level == -1) return;

//                        playerStats.attackDamage += UtilDefine.CalcPercent(playerStats.attackDamage,
//                                                            (numSum * (5 + GenealogyCnt)));
//                        playerStats.criticalPercent += UtilDefine.CalcPercent(playerStats.criticalPercent,
//                                                            (15 * (5 + saveCardCnt)));
//                        break;
//                    }

//            }
//        }


//    }
//}
