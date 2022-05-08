using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public abstract class CardPanal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected static CardInventoryManager _cardInventoryManager;

    private static bool _stopShowInfo;
    private static int _panalCount;

    protected CardOutLineEffect _outLineEffect;

    public Action OnChangeCardEvent { get; set; }

    protected CardData _currentCard { get; private set; }

    protected Image _currentImage;
    protected int _currentID;

    protected bool _isEmpty;
    protected bool _isDeferPanal;
    protected bool _isEventActive;

    #region 프로퍼티
    public CardData CurrentCard
    {
        get => _currentCard;
    }
    public bool IsEmpty
    {
        get => _isEmpty;
    }

    public bool IsDeferPanal
    {
        get => _isDeferPanal;
    }

    public int ID
    {
        get => _currentID;
    }


    #endregion

    private void Start()
    {
        _outLineEffect = GetComponent<CardOutLineEffect>();
        ChildStart();
    }

    protected abstract void ChildStart();

    public void Init()
    {
        if (_cardInventoryManager == null)
        {
            _cardInventoryManager = FindObjectOfType<CardInventoryManager>();
        }


        _currentID = _panalCount++;
        _currentImage = GetComponent<Image>();
        EmptyCard();
        ChildInit();

        AddedToGenealogyPanal();

        _cardInventoryManager.AddCardPanalList(this);
    }

    private void AddedToGenealogyPanal()
    {
        if (_isDeferPanal) return;

        GenealogyCardPanel panal = GetComponentInParent<GenealogyCardPanel>();
        panal.AddCardPanal(this);
    }

    protected abstract void ChildInit();

    public void ChangeCard(CardData cardData, bool isEffect = true)
    {
        _currentCard = cardData;
        _currentImage.sprite = _currentCard.CardSprite;

        if (_isEmpty)
        {
            _isEmpty = false;
            // HoldCard가 커지고 하게 하기

            if (isEffect)
            {
                transform.DOScale(Vector3.one * 3f, 0f);
                transform.DOScale(Vector3.one, 0.3f);
            }
        }

        OnChangeCardEvent?.Invoke();
        ChangeAlpha(1f);
    }

    public void ChangeCard(CardData cardData, Vector3 targetPos)
    {
        ChangeCard(cardData);
    }

    public void EmptyCard()
    {
        _currentCard = null;
        _currentImage.sprite = null;
        ChangeAlpha(0f);
        _isEmpty = true;
    }

    public void ChangeAlpha(float alpha)
    {
        Color color = _currentImage.color;
        color.a = alpha;
        _currentImage.color = color;
    }

    protected void SetShowInfo(bool isStop)
    {
        if (isStop)
        {
            _stopShowInfo = true;
            EventManager.TriggerEvent(Constant.EXIT_CARD_PANAL);
        }

        else
        {
            _stopShowInfo = false;
        }
    }

    public void AcitvePanal(bool isActive, bool isChangeEvent)
    {
        if (_isEmpty) return;
        _currentImage.raycastTarget = isActive;

        if (isActive)
        {
            ChangeAlpha(1f);
            _currentImage.color = Color.white;
            transform.DOScale(Vector3.one, 0.35f);
            _isEventActive = false;

            if (isChangeEvent)
            {
                transform.DOScale(Vector3.one * 1.3f, 0.5f);
                _outLineEffect.EffectStart();
                _isEventActive = true;
            }
        }

        else
        {

            ChangeAlpha(0.5f);
            _currentImage.color = Color.gray;
            _isEventActive = false;
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (_isEmpty || _stopShowInfo) return;

        Param param = new Param();
        param.sParam = _currentCard.ID;
        param.iParam = _currentID;
        param.vParam = transform.position;

        PEventManager.TriggerEvent(Constant.ENTER_CARD_PANAL, param);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (_isEmpty || _stopShowInfo) return;

        EventManager.TriggerEvent(Constant.EXIT_CARD_PANAL);
    }
}
