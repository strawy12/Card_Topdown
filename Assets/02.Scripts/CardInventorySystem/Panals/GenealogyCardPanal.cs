using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GenealogyDefine;

public class GenealogyCardPanal : MonoBehaviour
{
    private CardPanal[] _cardPanals = new CardPanal[2];
    private Text _genealogyText;

    private GenealogyData _genealogyData = null;

    private static int _genealogyCnt = 0;
    public GenealogyData Genealogy
    {
        get
        {
            return _genealogyData;
        }
    }

    public void Init()
    {
        _cardPanals = new CardPanal[2];
        _genealogyText = transform.Find("GenealogyText").GetComponent<Text>();
        _genealogyText.text = "";
    }

    public void AddCardPanal(CardPanal panal)
    {
        int idx = _cardPanals[0] == null ? 0 : 1;
        _cardPanals[idx] = panal;

        _cardPanals[idx].OnChangeCardEvent += ChangePanal;
    }

    private void ChangePanal()
    {
        if (_cardPanals[1].IsEmpty) return;



        CalcGenealogy(_cardPanals[0].CurrentCard, _cardPanals[1].CurrentCard);

        if(_genealogyData != null)
        {
            string pedi = GetGenealogyInfo(_genealogyData.genealogyType);
            string numStr = "";
            if (_genealogyData.genealogyType == EGenealogy.LightPair)
            {
                 numStr = LightDDangToString(_genealogyData.genealogyNum);
            }
            else
            {
                 numStr = NumberToString(_genealogyData.genealogyNum);
            }

            _genealogyText.text = $"{numStr}{pedi}";
            _genealogyText.gameObject.SetActive(true);
        }

        else
        {
            _genealogyText.gameObject.SetActive(false);
            _genealogyText.text = "";
        }
    }

    private void CalcGenealogy(CardData cardData1, CardData cardData2)
    {
        bool isSelect = false;
        int num1 = cardData1.CardNum;
        int num2 = cardData2.CardNum;

        int genealogyNum = 0;
        EGenealogy genealogyType = EGenealogy.None;

        if (num1 > num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        if (num1 == num2)
        {
            genealogyType = EGenealogy.Pair;
            genealogyNum = num1;
            isSelect = true;
        }

        else if (cardData1.IsLight && cardData2.IsLight)
        {
            genealogyType = EGenealogy.LightPair;
            genealogyNum = num1 + num2;
            isSelect = true;
        }

        else if (num1 == 4 && num2 == 6)
        {
            genealogyType = EGenealogy.SeRyuk;
            isSelect = true;
        }

        else if (num1 == 4 && num2 == 7)
        {
            genealogyType = EGenealogy.ESibal;
            isSelect = true;
        }

        else if (num1 == 4 && num2 == 10)
        {
            genealogyType = EGenealogy.Jangsa;
            isSelect = true;
        }

        else if (num1 == 1)
        {
            if (num2 == 9 || num2 == 10)
            {
                genealogyType = EGenealogy.BBing;
                genealogyNum = num2;
                isSelect = true;
            }

            else if (num2 == 2)
            {
                genealogyType = EGenealogy.Ali;
                isSelect = true;
            }

            else if (num2 == 4)
            {
                genealogyType = EGenealogy.Doksa;
                isSelect = true;
            }

        }

        if (!isSelect)
        {
            if ((num1 + num2) == 10)
            {
                genealogyType = EGenealogy.Mangtong;
            }

            else
            {
                genealogyType = EGenealogy.Rest;
                genealogyNum = (num1 + num2) % 10;
            }
        }


        _genealogyData = new GenealogyData(genealogyType, genealogyNum);

        GameManager.Inst.Data.SetGenealogyData(_genealogyData);

        if(_genealogyCnt++ != 0)
        {
            Param param = new Param();
            //param.iParam = (int)genealogyType;
            param.iParam = _genealogyCnt;

            PEventManager.TriggerEvent("CardAdd", param);
        }
    }

    private void CloseMessage()
    {

    }
}
