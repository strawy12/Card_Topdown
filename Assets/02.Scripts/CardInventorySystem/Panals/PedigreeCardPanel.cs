using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class PedigreeCardPanel : MonoBehaviour
{
    private CardPanal[] _cardPanals = new CardPanal[2];
    private Text _pedigreeText;

    private PedigreeData _pedigreeData = null;

    public PedigreeData Pedigree
    {
        get
        {
            return _pedigreeData;
        }
    }

    public void Init()
    {
        _cardPanals = new CardPanal[2];
        _pedigreeText = transform.Find("PedigreeText").GetComponent<Text>();
        _pedigreeText.text = "";
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

        CalcPedigree(_cardPanals[0].CurrentCard, _cardPanals[1].CurrentCard);

        if(_pedigreeData != null)
        {
            string pedi = GetPedigreeInfo(_pedigreeData.pedigreeType);
            string numStr = "";
            if (_pedigreeData.pedigreeType == EPedigree.LightDDang)
            {
                 numStr = NumberToString(_pedigreeData.pedigreeNum);
            }
            else
            {
                 numStr = NumberToString(_pedigreeData.pedigreeNum);
            }

            _pedigreeText.text = $"{numStr}{pedi}";
            _pedigreeText.gameObject.SetActive(true);
        }

        else
        {
            _pedigreeText.gameObject.SetActive(false);
            _pedigreeText.text = "";
        }
    }

    private void CalcPedigree(CardData cardData1, CardData cardData2)
    {
        bool isSelect = false;
        int num1 = cardData1.CardNum;
        int num2 = cardData2.CardNum;

        int pedigreeNum = 0;
        EPedigree pedigreeType = EPedigree.None;

        if (num1 > num2)
        {
            Debug.Log(num1 + " "+ num2);
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        if (num1 == num2)
        {
            pedigreeType = EPedigree.Pair;
            pedigreeNum = num1;
            isSelect = true;
        }

        else if(cardData1.IsLight && cardData2.IsLight)
        {
            pedigreeType = EPedigree.LightDDang;
            pedigreeNum = num1+num2;
            isSelect = true;
        }

        else if(num1 == 4 && num2 == 6)
        {
            pedigreeType = EPedigree.SeRyuk;
            isSelect = true;
        }

        else if(num1 == 4 && num2 == 7)
        {
            pedigreeType = EPedigree.ESibal;
            isSelect = true;
        }

        else if (num1 == 4 && num2 == 10)
        {
            pedigreeType = EPedigree.Jangsa;
            isSelect = true;
        }

        else if(num1 == 1)
        {
            if(num2 == 9 || num2 == 10)
            {
                pedigreeType = EPedigree.BBing;
                pedigreeNum = num2;
                isSelect = true;
            }

            else if(num2 == 2)
            {
                pedigreeType = EPedigree.Ali;
                isSelect = true;
            }

            else if(num2 == 4)
            {
                pedigreeType = EPedigree.Doksa;
                isSelect = true;
            }

        }

        if(!isSelect)
        {
            if ((num1 + num2) == 10)
            {
                pedigreeType = EPedigree.MangTong;
            }

            else
            {
                pedigreeType = EPedigree.Rest;
                pedigreeNum = (num1 + num2) % 10;
            }
        }
      

        _pedigreeData = new PedigreeData(pedigreeType, pedigreeNum);

        GameManager.Inst.Data.SetPedigreeData(_pedigreeData);
    }

}
