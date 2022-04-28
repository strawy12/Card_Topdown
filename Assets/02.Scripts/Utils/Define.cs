using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
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
    public const string SELECT_CARD = "SL_CD";
    public const string UNSELECT_CARD = "USL_CD";

    public const string ENTER_CARDPANAL = "ET_CDP";
    public const string EXIT_CARDPANAL = "EX_CDP";

}
