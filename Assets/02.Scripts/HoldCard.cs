using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class HoldCard : MonoBehaviour
{
    private Image _cardImage;
    private Vector3 _returnPos;
    private int _returnPanalIndex;
    private CardData _cardData;

    private bool _holdCard;
    private bool _isReturnCard;

    private void Start()
    {
        PEventManager.StartListening(Constant.POINTDOWN_CARD, StartHold);
        PEventManager.StartListening(Constant.RETURN_CARD_EFFECT, ReturnCardEffect);
        EventManager.StartListening(Constant.TRIGGER_MOUNTING_EVENT, ResetHoldCard);
        EventManager.StartListening(Constant.NOT_ENTER_MOUNTING_UI, ReturnCard);
    }

    private void Update()
    {
        if (_holdCard)
        {
            transform.position = Define.MousePos;


            if (Input.GetMouseButtonUp(0))
            {
                OnPointerUp();
            }
        }

    }

    private void StartHold(Param param)
    {
        if (_isReturnCard)
        {
            ImmatiateReturn();
        }

        Init(param);
        _holdCard = true;
    }

    public void OnPointerUp()
    {
        Param param = new Param();
        param.sParam = _cardData.ID;
        param.iParam = _returnPanalIndex;
        param.vParam = Define.MousePos;

        _holdCard = false;

        PEventManager.TriggerEvent(Constant.POINTUP_CARD, param);
    }

    private void Init(Param param)
    {
        if (_cardImage == null)
        {
            _cardImage = GetComponent<Image>();
        }

        _returnPos = param.vParam;
        _cardData = GameManager.Inst.FindCardDataWithID(param.sParam);
        _returnPanalIndex = param.iParam;
        _cardImage.sprite = _cardData.CardSprite;
        _cardImage.enabled = true;
    }

    private void ReturnCardEffect(Param param)
    {
        _holdCard = false;
        _returnPos = param.vParam;
        _cardData = GameManager.Inst.FindCardDataWithID(param.sParam);
        _returnPanalIndex = param.iParam;
        _cardImage.sprite = _cardData.CardSprite;
        _cardImage.enabled = true;

        ReturnCard();
    }

    private void ReturnCard()
    {
        _cardImage.enabled = true;

        _holdCard = false;
        _isReturnCard = true;
        transform.DOKill();
        transform.DOMove(_returnPos, 0.8f).OnComplete(EndReturnCard);
    }

    private void ImmatiateReturn()
    {
        transform.DOKill();
        EndReturnCard();
    }

    private void EndReturnCard() 
    {
        Param param = new Param();
        param.sParam = _cardData.ID;
        param.iParam = _returnPanalIndex;

        PEventManager.TriggerEvent(Constant.RETURN_CARD, param);

        ResetHoldCard();
    }

    private void ResetHoldCard()
    {
        _holdCard = false;
        _returnPos = Vector3.zero;
        _cardImage.enabled = false;
        _cardData = null;
        _isReturnCard = false;
    }

    private void OnDestroy()
    {
        PEventManager.StopListening(Constant.POINTDOWN_CARD, Init);
    }

}
