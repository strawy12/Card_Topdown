using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum EButtonStyle
    {
        None = -1,
        Okay,
        Cancel,
        Close
    }

    public enum EPedigree
    {
        None,
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
        MangTong,
        LightDDang
    };


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

    private static Dictionary<EPedigree, string> _pedigreInfoDict;
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

    public static string GetPedigreeInfo(EPedigree pedigree)
    {
        if (_pedigreInfoDict == null)
        {
            InitPedigreeInfoDict();
        }

        if (!_pedigreInfoDict.ContainsKey(pedigree))
        {
            return null;
        }

        return _pedigreInfoDict[pedigree];
    }
    private static void InitNumberToStringArray()
    {
        _numberToStringArray = new Dictionary<int, string>();

        _numberToStringArray.Add(0, "��");
        _numberToStringArray[1] = "��";
        _numberToStringArray[2] = "��";
        _numberToStringArray[3] = "��";
        _numberToStringArray[4] = "��";
        _numberToStringArray[5] = "��";
        _numberToStringArray[6] = "ĥ";
        _numberToStringArray[7] = "��";
        _numberToStringArray[8] = "��";
        _numberToStringArray[9] = "��";
    }


    private static void InitPedigreeInfoDict()
    {
        _pedigreInfoDict = new Dictionary<EPedigree, string>();
        _pedigreInfoDict.Add(EPedigree.Pair, "��");
        _pedigreInfoDict.Add(EPedigree.PairHunter, "������");
        _pedigreInfoDict.Add(EPedigree.Rest, "��");
        _pedigreInfoDict.Add(EPedigree.SeRyuk, "����");
        _pedigreInfoDict.Add(EPedigree.Ali, "�˸�");
        _pedigreInfoDict.Add(EPedigree.BBing, "��");
        _pedigreInfoDict.Add(EPedigree.Doksa, "����");
        _pedigreInfoDict.Add(EPedigree.ESibal, "������");
        _pedigreInfoDict.Add(EPedigree.GabO, "����");
        _pedigreInfoDict.Add(EPedigree.GuSa, "����");
        _pedigreInfoDict.Add(EPedigree.Jangsa, "���");
        _pedigreInfoDict.Add(EPedigree.LightDDang, "����");
        _pedigreInfoDict.Add(EPedigree.MangTong, "����");
    }

    private static void InitCardInfoDatas()
    {
        _cardInfoDatas = new string[10];

        _cardInfoDatas[0] = "��";
        _cardInfoDatas[1] = "����";
        _cardInfoDatas[2] = "�����";
        _cardInfoDatas[3] = "��θ�";
        _cardInfoDatas[4] = "��";
        _cardInfoDatas[5] =  "���";
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
    
    public const string CLOSE_MESSAGE= "CL_MS";

    public const string MESSAGE_MOUNTING = "������ �����Ͻðڽ��ϱ�?";



}
