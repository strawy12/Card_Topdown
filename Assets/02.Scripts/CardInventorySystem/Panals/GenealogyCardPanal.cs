using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static GenealogyDefine;
using System;

public class GenealogyCardPanal : MonoBehaviour, IUIEvent
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

    public bool IsEmpty
    {
        get => _genealogyData == null;
    }

    public bool CanEnForce
    {
        get
        {
            return _currentEnforceCnt >= _needEnforceCnt;
        }
    }


    public Action<Param> OnPointerUpUIEnter { get; set; }
    public Action<Param> OnPointerUpUINotEnter { get; set; }

    private int _currentEnforceCnt = 0;
    private int _needEnforceCnt = 0;

    public void Init()
    {
        _cardPanals = new CardPanal[2];
        _genealogyText = transform.Find("GenealogyText").GetComponent<Text>();
        _genealogyText.text = "";

        SubscribeEvent();

    }

    private void SubscribeEvent()
    {

        IUIEvent uiEvent = transform.Find("CheckEnterUI").GetComponent<IUIEvent>();

        uiEvent.OnPointerUpUIEnter += EnterUI;
        uiEvent.OnPointerUpUINotEnter += OnPointerUpUINotEnter;
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

        if (_genealogyData != null)
        {
            SetGenealogyText();
            _genealogyText.gameObject.SetActive(true);
            return;
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

        }

        else
        {
            _genealogyText.gameObject.SetActive(false);
            _genealogyText.text = "";
        }
    }

    private void SetGenealogyText()
    {
        _genealogyText.text = $"+ {_currentEnforceCnt}";
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

        //else if (cardData1.IsLight && cardData2.IsLight)
        //{
        //    genealogyType = EGenealogy.LightPair;
        //    genealogyNum = num1 + num2;
        //    isSelect = true;
        //}

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

        // GameManager.Inst.Data.SetGenealogyData(_genealogyData);

        Param param = new Param();
        //param.iParam = (int)genealogyType;
        param.iParam = _genealogyCnt;

        PEventManager.TriggerEvent("CardAdd", param);
        _genealogyCnt++;
    }

    private void CloseMessage()
    {

    }

    private void EnterUI(Param param)
    {
        if (!IsEmpty && CheckEnforceMaterial(param.sParam))
        {
            EnForceMountMessage(param);
        }

        else
        {
            OnPointerUpUIEnter?.Invoke(param);
        }


    }

    private bool CheckEnforceMaterial(string id)
    {
        for (int i = 0; i < _cardPanals.Length; i++)
        {
            if (!_cardPanals[i].IsEmpty && _cardPanals[i].CurrentCard.ID.Equals(id))
            {
                return true;
            }
        }

        return false;
    }

    private void EnForceMountMessage(Param param)
    {
        ButtonStyle btn1 = new ButtonStyle(UtilDefine.EButtonStyle.Okay, () =>
        {
            _currentEnforceCnt++;
            SetGenealogyText();
        });

        ButtonStyle btn2 = new ButtonStyle(UtilDefine.EButtonStyle.Cancel, () =>
        {
            PEventManager.TriggerEvent(Constant.RETURN_CARD_EFFECT, param);
        });

        GameManager.Inst.UI.TriggerMessage(Constant.MESSAGE_MOUNTING, btn1, btn2);
    }
}
