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

    private static Dictionary<int, string> _cardInfoDict;

    public static string GetCardInfo(int num)
    {
        if(_cardInfoDict == null)
        {
            InitDict();
        }

        if(!_cardInfoDict.ContainsKey(num))
        {
            Debug.LogError("1~10�� ���� ���� �ٸ� ���ڸ� �Է��ϼ̽��ϴ�.");
            return null;
        }

        return _cardInfoDict[num];
    }
    
    public static void InitDict()
    {
        _cardInfoDict = new Dictionary<int, string>();
        _cardInfoDict.Add(1, "��");
        _cardInfoDict.Add(2, "����");
        _cardInfoDict.Add(3, "�����");
        _cardInfoDict.Add(4, "��θ�");
        _cardInfoDict.Add(5, "��");
        _cardInfoDict.Add(6, "���");
        _cardInfoDict.Add(7, "ȫ�θ�");
        _cardInfoDict.Add(8, "����");
        _cardInfoDict.Add(9, "����");
        _cardInfoDict.Add(10, "ǳ");
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
