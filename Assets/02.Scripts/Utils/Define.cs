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
            Debug.LogError("1~10의 숫자 외의 다른 숫자를 입력하셨습니다.");
            return null;
        }

        return _cardInfoDict[num];
    }
    
    public static void InitDict()
    {
        _cardInfoDict = new Dictionary<int, string>();
        _cardInfoDict.Add(1, "솔");
        _cardInfoDict.Add(2, "매조");
        _cardInfoDict.Add(3, "사쿠라");
        _cardInfoDict.Add(4, "흑싸리");
        _cardInfoDict.Add(5, "초");
        _cardInfoDict.Add(6, "목단");
        _cardInfoDict.Add(7, "홍싸리");
        _cardInfoDict.Add(8, "공산");
        _cardInfoDict.Add(9, "국진");
        _cardInfoDict.Add(10, "풍");
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

    public const string MESSAGE_MOUNTING = "정말로 장착하시겠습니까?";



}
