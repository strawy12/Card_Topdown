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

    public CardData CurrentCard
    {
        get => _currentCard;
        set
        {
            if (value != null)
            {
                Debug.LogError("Error! DrawCardSelectPanal.CurrentCard Set Value Not Null");
                return;
            }

            _currentCard = null;
        }
    }

    public void InitPanal(CardData cardData, ECardType type, System.Action<int> action)
    {
        if (cardData == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if(_currentImage == null)
        {
            _currentImage = GetComponentInChildren<Image>();
        }

        if(_currentText == null)
        {
            _currentText = GetComponentInChildren<Text>();
        }

        gameObject.SetActive(true);
        _currentCard = cardData;

        _currentImage.sprite = _currentCard.CardSprite;
        _currentText.text = string.Format("{0}월\n[{1}]", _currentCard.CardNum,
                            type == ECardType.Defer ? "보류카드" : "뽑은 카드");

        onClick.AddListener(() => action?.Invoke(transform.GetSiblingIndex()));

        gameObject.SetActive(true);
    }

}
