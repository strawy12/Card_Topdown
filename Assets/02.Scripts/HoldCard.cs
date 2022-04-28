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
    private CardData _cardData;

    private bool _holdCard;

    private void Start()
    {
        PEventManager.StartListening(Constant.SELECT_CARD, Init);
    }

    private void Update()
    {
        if(_holdCard)
        {
            transform.position = Define.MousePos;

                
            if (Input.GetMouseButtonUp(0))
            {
                OnPointerUp();
            }

        }

    }

    private void Init(Param param)
    {
        if (_cardImage == null)
            _cardImage = GetComponent<Image>();

        _returnPos = param.vParam;
        _cardData = GameManager.Inst.FindCardDataWithID(param.sParam);

        _cardImage.sprite = _cardData.CardSprite;
        _cardImage.enabled = true;
        _holdCard = true;
    }

    public void OnPointerUp()
    {
        _holdCard = false;

        Param param = new Param();
        param.vParam = transform.position;

        ReturnCardPos();
        PEventManager.TriggerEvent(Constant.UNSELECT_CARD, param);
    }

    private void ReturnCardPos()
    {
        transform.DOMove(_returnPos, 0.8f).OnComplete(()=> _cardImage.enabled = false);
    }

    private void OnDestroy()
    {
        PEventManager.StopListening(Constant.SELECT_CARD, Init);
    }

}
