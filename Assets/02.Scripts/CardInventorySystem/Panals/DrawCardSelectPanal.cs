using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardSelectPanal : Button
{
    public enum ECardType
    {
        Defer,
        NewPick
    }

    private Image _currentImage;
    private Text _currentText;

    private CardData _currentCard;

    private CardPanal _currentCardPanal;

    public CardData CurrentCard
    {
        get => _currentCard;
    }

    public CardPanal CurrentCardPanal
    {
        get => _currentCardPanal;
    }


    public void InitPanal(CardData cardData, ECardType type, System.Action<int> action, int idx, CardPanal panal = null)
    {
        if (_currentImage == null)
        {
            _currentImage = GetComponentInChildren<Image>();
        }

        if (_currentText == null)
        {
            _currentText = GetComponentInChildren<Text>();
        }

        if (type == ECardType.Defer)
        {
            _currentCardPanal = panal;
        }

        _currentCard = cardData;

        _currentImage.sprite = _currentCard.CardSprite;
        _currentText.text = string.Format("{0}월\n[{1}]", _currentCard.CardNum,
                            type == ECardType.Defer ? "보류카드" : "뽑은 카드");

        onClick.AddListener(() => action?.Invoke(idx));

        this.interactable = true;
        gameObject.SetActive(true);
    }

    public void ReleasePanal()
    {
        _currentCard = null;
        _currentCardPanal = null;
        onClick.RemoveAllListeners();
    }

}
