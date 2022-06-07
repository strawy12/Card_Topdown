using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static Constant;

public class DeferCardPanal : CardPanal, IPointerDownHandler
{
    [SerializeField] private TMP_Text _countText;
    private int _currentCnt = 0;
    
    protected override void ChildStart()
    {
        if (_countText == null)
        {
            _countText = transform.Find("CountText").GetComponent<TMP_Text>();
        }

        PEventManager.StartListening(RETURN_CARD, ActiveShowInfo);
    }

    protected override void ChildInit()
    {
        _isDeferPanal = true;

        if(_isEmpty)
        {
            _currentCnt = 0;
        }

        SetCountText();
    }

    private void ActiveShowInfo(Param param)
    {
        if (_currentID != param.iParam) return;
        ChangeCard(GameManager.Inst.FindCardDataWithID(param.sParam), false);
        SetShowInfoActive(isStop:false);
    } 

    public override void ChangeCard(CardData cardData, bool isEffect = true)
    {
        if(!_isEmpty && cardData.ID.Equals(_currentCard.ID))
        {
            _currentCnt++;
            SetCountText();
            if(isEffect)
            {
                CardAddEffect();
            }
        }

        else
        {
            base.ChangeCard(cardData, isEffect);

            if(!_isEmpty)
            {
                _currentCnt = 1;
                _countText.enabled = true;
                SetCountText();
            }

            else
            {
                _currentCnt = 0;
                _countText.enabled = false;
            }
        }
    }
    protected override void ChildEmptyCard()
    {
        _currentCnt = 0;
        _countText.enabled = false; 
    }

    private void SetCountText()
    {
        _countText.text = $"<size=12>x</size>{_currentCnt}";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isEmpty || _isEventActive) return;

        SetShowInfoActive(isStop:true);

        Param param = new Param();
        param.vParam = transform.position;
        param.sParam = _currentCard.ID;
        param.iParam = _currentID;

        PEventManager.TriggerEvent(POINTDOWN_CARD, param);

        if(--_currentCnt == 0)
        {
            EmptyCard();
        }

        SetCountText();
    }

}
