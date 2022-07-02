using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OwnCardPanel : CardPanel
{
    [SerializeField] private TMP_Text _countText;
    private int _currentCount;

    protected override void ChildInit()
    {
        if (_countText == null)
            _countText = GetComponent<TMP_Text>();

        _panelType = ECardPanelType.Own;
        _currentCount = 0;
        SetCountText();
    }

    public override void ChangeCard(CardData cardData, bool isEffect = true)
    {
        if (!_isEmpty && cardData.ID.Equals(_currentCardData.ID))
        {
            _currentCount++;
            SetCountText();
            if (isEffect)
            {
                CardAddEffect();
            }
        }

        else
        {
            base.ChangeCard(cardData, isEffect);

            if (!_isEmpty)
            {
                _currentCount = 1;
                _countText.enabled = true;
                SetCountText();
            }

            else
            {
                _currentCount = 0;
                _countText.enabled = false;
            }
        }
    }

    public override void EmptyCard()
    {
        base.EmptyCard();

        _currentCount = 0;
        _countText.enabled = false;
    }

    private void SetCountText()
    {
        _countText.text = $"<size=12>x</size>{_currentCount}";
    }
}
