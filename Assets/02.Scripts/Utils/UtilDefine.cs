using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UtilDefine
{
    public enum EButtonStyle
    {
        None = -1,
        Okay,
        Cancel,
        Close
    }

    private static Camera mainCam;
    public static Camera MainCam
    {
        get
        {
            mainCam ??= Camera.main;
            return mainCam;
        }
    }

    public static Vector3 MousePos
    {
        get
        {
            Vector3 pos = MainCam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            return pos;
        }
    }

    public static float CalcPercent(float value, float percent)
    {
        return (value * 100) / percent;
    }
}

public class GenealogyDefine
{
    public enum EGenealogy
    {
        None = -1,
        Rest,
        Pair,
        GuSa,
        PairHunter,
        ESibal,
        SeRyuk,
        Jangsa,
        BBing,
        Doksa,
        Ali,
        GabO,
        Mangtong,
        LightPair
    };

    public enum ESynergy
    {
        None = -1,
        Rest,
        Pair,
        LightPair,
        Middle,
        Special,
        Mangtong,
        Count
    }

    private static Dictionary<EGenealogy, string> _pedigreInfoDict;
    private static string[] _cardInfoDatas;
    private static Dictionary<int, string> _numberToStringArray;

    public static string NumberToString(int num)
    {
        if (num > 10 || num < 1)
        {
            return "";
        }

        num--;
        if (_numberToStringArray == null)
        {
            InitNumberToStringArray();
        }


        return _numberToStringArray[num];
    }

    public static string LightDDangToString(int num)
    {
        switch(num)
        {

            case 4:
                return "�ϻ�";

            case 9:
                return "����";

            case 11:
                return "����";
        }

        return "";
    }

    public static string GetCardInfo(int num)
    {
        if (num > 10 || num < 1)
        {
            return "";
        }

        num--;

        if (_cardInfoDatas == null)
        {
            InitCardInfoDatas();
        }



        return _cardInfoDatas[num];
    }

    public static string GetGenealogyInfo(EGenealogy Genealogy)
    {
        if (_pedigreInfoDict == null)
        {
            InitGenealogyInfoDict();
        }

        if (!_pedigreInfoDict.ContainsKey(Genealogy))
        {
            return null;
        }

        return _pedigreInfoDict[Genealogy];
    }

    

    private static void InitNumberToStringArray()
    {
        _numberToStringArray = new Dictionary<int, string>();

        _numberToStringArray.Add(0, "��");
        _numberToStringArray.Add(1, "��");
        _numberToStringArray.Add(2, "��");
        _numberToStringArray.Add(3, "��");
        _numberToStringArray.Add(4, "��");
        _numberToStringArray.Add(5, "��");
        _numberToStringArray.Add(6, "ĥ");
        _numberToStringArray.Add(7, "��");
        _numberToStringArray.Add(8, "��");
        _numberToStringArray.Add(9, "��");
    }

        private static void InitGenealogyInfoDict()
    {
        _pedigreInfoDict = new Dictionary<EGenealogy, string>();
        _pedigreInfoDict.Add(EGenealogy.Pair, "��");
        _pedigreInfoDict.Add(EGenealogy.PairHunter, "������");
        _pedigreInfoDict.Add(EGenealogy.Rest, "��");
        _pedigreInfoDict.Add(EGenealogy.SeRyuk, "����");
        _pedigreInfoDict.Add(EGenealogy.Ali, "�˸�");
        _pedigreInfoDict.Add(EGenealogy.BBing, "��");
        _pedigreInfoDict.Add(EGenealogy.Doksa, "����");
        _pedigreInfoDict.Add(EGenealogy.ESibal, "������");
        _pedigreInfoDict.Add(EGenealogy.GabO, "����");
        _pedigreInfoDict.Add(EGenealogy.GuSa, "����");
        _pedigreInfoDict.Add(EGenealogy.Jangsa, "���");
        _pedigreInfoDict.Add(EGenealogy.LightPair, "����");
        _pedigreInfoDict.Add(EGenealogy.Mangtong, "����");
    }

    private static void InitCardInfoDatas()
    {
        _cardInfoDatas = new string[10];

        _cardInfoDatas[0] = "��";
        _cardInfoDatas[1] = "����";
        _cardInfoDatas[2] = "�����";
        _cardInfoDatas[3] = "��θ�";
        _cardInfoDatas[4] = "��";
        _cardInfoDatas[5] = "���";
        _cardInfoDatas[6] = "ȫ�θ�";
        _cardInfoDatas[7] = "����";
        _cardInfoDatas[8] = "����";
        _cardInfoDatas[9] = "ǳ";
    }
}

public static class Constant
{
    public const string POINTDOWN_CARD = "PD_CD";
    public const string POINTUP_CARD = "PU_CD";
    public const string CLICK_CARD = "CL_CD";
    public const string RETURN_CARD = "RT_CD";


    public const string RETURN_CARD_EFFECT = "RT_CD_EF";

    public const string ENTER_MOUNTING_UI = "ET_MT_UI";
    public const string NOT_ENTER_MOUNTING_UI = "N_ET_MT_UI";

    public const string ENTER_UI = "ET_UI";
    public const string EXIT_UI = "EX_UI";

    public const string ENTER_CARD_PANAL = "ET_CD_PL";
    public const string EXIT_CARD_PANAL = "EX_CD_PL";

    public const string TRIGGER_CHANGE_EVENT = "TG_CG_EV";
    public const string TRIGGER_MOUNTING_EVENT = "TG_MT_EV";
    public const string TRIGGER_MONSTER_DEAD = "TR_MS_DD";
    public const string TRIGGER_ADD_CARD = "TR_AD_CD";
    public const string TRIGGER_PICK_CARD = "TR_PC_CD";

    public const string CLOSE_MESSAGE= "CL_MS";

    public const string MESSAGE_MOUNTING = "������ �����Ͻðڽ��ϱ�?";

    public const string OPEN_INVENTORY = "OP_IV";
    public const string PLAYER_ATTACK_START = "PL_AT_ST";
    public const string PLAYER_ATTACK_END = "PL_AT_ED";




}
