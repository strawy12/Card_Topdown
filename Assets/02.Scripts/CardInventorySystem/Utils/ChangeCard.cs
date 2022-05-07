using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeCard : MonoBehaviour
{
    private Image _currentImage;
    private CardData _currentCard;
    private CardPanal _currentPanal;
    
    public void Init(Vector3 initPos, CardPanal targetPanal, CardData card)
    {
        if (_currentImage == null)
        {
            _currentImage = GetComponent<Image>();
        }

        transform.position = initPos;

        _currentPanal = targetPanal;
        _currentCard = card;

        _currentImage.sprite = card.CardSprite;
        _currentImage.enabled = true;

        transform.DOMove(targetPanal.transform.position, 0.8f).OnComplete(EndMove);

    }

    private void EndMove()
    {
        _currentPanal.AcitvePanal(true, false);
        _currentPanal.ChangeCard(_currentCard);
        Destroy(gameObject);
    }
}
