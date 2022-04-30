using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Constant;

public class DeferCardPanal : CardPanal, IPointerDownHandler
{
    protected override void ChildStart()
    {
        PEventManager.StartListening(RETURN_CARD, ActiveShowInfo);
    }

    protected override void ChildInit()
    {
        _isDeferPanal = true;
    }

    private void ActiveShowInfo(Param param)
    {
        if (_currentID != param.iParam) return;
        CardData card = GameManager.Inst.FindCardDataWithID(param.sParam);
        ChangeCard(card);

        SetShowInfo(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isEmpty) return;

        SetShowInfo(true);

        Param param = new Param();
        param.vParam = transform.position;
        param.sParam = _currentCard.ID;
        param.iParam = _currentID;

        PEventManager.TriggerEvent(POINTDOWN_CARD, param);

        EmptyCard();
    }

}
