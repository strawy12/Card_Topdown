using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldCard : MonoBehaviour
{
    private Image _cardImage;
    private Vector3 _returnPos;
    private string _returnPanelID;
    private CardData _cardData;
    private bool _holdCard;
    private bool _isReturnCard;

    private void Update()
    {
        if (_holdCard)
        {
            transform.position = UtilDefine.MousePos;


            if (Input.GetMouseButtonUp(0))
            {
                OnPointerUp();
            }
        }

    }

    public void StartHold(string panelID, CardData data, Vector2 panelPos)
    {
        if (_cardImage == null)
        {
            _cardImage = GetComponent<Image>();
        }

        if (_isReturnCard)
        {
            ImmatiateReturn();
        }

        _returnPos = panelPos;
        _cardData = data;
        _returnPanelID = panelID;

        _cardImage.sprite = _cardData.CardSprite;
        _cardImage.enabled = true;
        _holdCard = true;
    }

    public void OnPointerUp()
    {
        _cardImage.enabled = false;
        _holdCard = false;

        CardInventoryManager.Inst.EndHold(_returnPanelID,_cardData, _returnPos);
    }


    public void ReturnCard(string panelID, CardData data, Vector2 returnPos)
    {
        _cardImage.enabled = true;

        _holdCard = false;
        _isReturnCard = true;
        transform.DOKill();
        transform.DOMove(returnPos, 0.8f).OnComplete(() => EndReturnCard(panelID, data)).SetUpdate(true);
    }

    private void ImmatiateReturn()
    {
        transform.DOKill(true);
        EndReturnCard(_returnPanelID, _cardData);
    }

    private void EndReturnCard(string panelID, CardData data)
    {
        CardInventoryManager.Inst.SetPanel(data, panelID);
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

}
