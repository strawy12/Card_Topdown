using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCardPanals : GeneratePanals
{
    protected override void ChildSettingPanal(GameObject panal)
    {
        CardPanal cardPanal = panal.GetComponent<CardPanal>();
        cardPanal.Init();
    }
}
