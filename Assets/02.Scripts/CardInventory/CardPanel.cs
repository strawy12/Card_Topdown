using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ECardPanelType
{
    Equip,
    Own
}


public abstract class CardPanel : MonoBehaviour
{
    protected CardData _currentCardData;
    protected Image _cardImage;

    protected bool _isEmpty;
    protected ECardPanelType _panelType;

    protected int _currentIdx;

    public string ID => $"{_panelType}_{_currentIdx}";
    public int Idx => _currentIdx;
    public ECardPanelType Type => _panelType;
    public bool IsEmpty => _isEmpty;
    public CardData CurrentCardData => _currentCardData;

    public void Init()
    {
        if (_cardImage == null)
        {
            _cardImage = GetComponent<Image>();
        }

        
        ChildInit();
        EmptyCard();

        CardInventoryManager.Inst.AddCardPanel(this);
    }
    protected virtual void ChildInit() { }

    public virtual void ChangeCard(CardData data, bool isEffect = true)
    {
        _currentCardData = data;
        _cardImage.sprite = _currentCardData.CardSprite;

        if(_isEmpty)
        {
            _isEmpty = false;

            if(isEffect && CardInventoryManager.Inst.IsActive)
            {
                CardAddEffect();
            }

            ChangeAlpha(1f);
        }
    }

    protected void CardAddEffect()
    {
        transform.localScale = (Vector3.one * 3f);
        transform.DOScale(Vector3.one, 0.3f).SetUpdate(true);
        GameManager.Inst.UI.PlayAddCardSound();
    }
    public virtual void EmptyCard()
    {
        _currentCardData = null;
        _cardImage.sprite = null;
        ChangeAlpha(0f);
        _isEmpty = true;
    }

    public void ChangeAlpha(float alpha)
    {
        Color color = _cardImage.color;
        color.a = alpha;
        _cardImage.color = color;
    }

}
