using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class MountCardPanal : CardPanal//, IPointerClickHandler
{
    private static MountingUI _moutingUI;
    protected override void ChildInit()
    {
        if(_moutingUI == null)
        {
            _moutingUI = transform.GetComponentInParent<MountingUI>();
        }
        _isDeferPanal = false;
    }

    protected override void ChildStart()
    {
    }

    public override void ChangeCard(CardData cardData, bool isEffect = true)
    {
        base.ChangeCard(cardData, isEffect);

        if (_currentIdx <= 1)
        {
            GameManager.Inst.SetPlayerableCardInfo(_currentIdx, _currentCard.ID);
        }
    }
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (_isEmpty || !_isEventActive) return;

    //    Param param = new Param();
    //    param.iParam = _currentID;
    //    param.sParam = _currentCard.ID;

    //    PEventManager.TriggerEvent(Constant.CLICK_CARD, param);
    //}


}
