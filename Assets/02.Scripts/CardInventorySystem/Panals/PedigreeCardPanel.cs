using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PedigreeCardPanel : MonoBehaviour
{
    private CardPanal[] _cardPanals = new CardPanal[2];

    private EPedigree _pedigreeType = EPedigree.None;

    public EPedigree Pedigree
    {
        get
        {
            return _pedigreeType;
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _cardPanals = GetComponentsInChildren<CardPanal>();
        

    }
    // ¾Õ¸Ó¸®°¡ ±æ°í ¾È°æ ½è°í
    // µ¿À±ÀÌ¶û ºñ½ÁÇÏ°Ô »ý±è
    // 
    private void ChangePanal()
    {
        CalcPedigree(_cardPanals[0].CurrentCard, _cardPanals[1].CurrentCard);
    }

    private void CalcPedigree(CardData cardData1, CardData cardData2)
    {
        int num1 = cardData1.CardNum;
        int num2 = cardData2.CardNum;

        if(num1 > num2)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        if (num1 == num2)
        {
            _pedigreeType = EPedigree.Pair;
        }

        else if(cardData1.IsLight && cardData2.IsLight)
        {
            _pedigreeType = EPedigree.LightDDang;
        }

        else if(num1 == 4 && num2 == 6)
        {
            _pedigreeType = EPedigree.SeRyuk;
        }

        else if(num1 == 4 && num2 == 10)
        {
            _pedigreeType = EPedigree.Jangsa;
        }

        else if (num1 == 4 && num2 == 10)
        {
            _pedigreeType = EPedigree.Jangsa;
        }

        else if(num1 == 1)
        {
            if(num2 == 9 || num2 == 10)
            {
                _pedigreeType = EPedigree.BBing;
            }

            else if(num2 == 2)
            {
                _pedigreeType = EPedigree.Ali;
            }

            else if(num2 == 4)
            {
                _pedigreeType = EPedigree.Doksa;
            }
        }

        else
        {
            _pedigreeType = EPedigree.Rest;
        }
    }    
}
