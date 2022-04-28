using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Constant;

public class HoldCardPanal : CardPanal, IPointerDownHandler
{
    protected override void ChildStart()
    {
        PEventManager.StartListening(UNSELECT_CARD, ActiveShowInfo);
    }

    protected override void ChildInit()
    {
        _isHoldPanal = true;
    }

    private void ActiveShowInfo(Param param)
    {
        SetShowInfo(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetShowInfo(true);

        Param param = new Param();
        param.vParam = transform.position;
        param.sParam = _currentCard.ID;

        PEventManager.TriggerEvent(SELECT_CARD, param);

        EmptyCard();
    }

}
