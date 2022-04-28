using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class CardPanal : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static bool _stopShowInfo;

    protected bool _isHoldPanal;

    protected CardData _currentCard;
    protected Image _currentImage;
    protected int _currentIdx;

    protected bool _isEmpty;
    public bool IsEmpty
    {
        get => _isEmpty;
    }

    private void Start()
    {
        Init(GameManager.Inst.GetCardData(0), 0);
        ChildStart();
    }

    protected abstract void ChildStart();

    public void Init(CardData cardData, int idx)
    {
        _currentIdx = idx;

        if (cardData == null)
        {
            EmptyCard();
        }

        else
        {
            _currentCard = new CardData(cardData);
            _currentImage = GetComponent<Image>();
            _currentImage.sprite = _currentCard.CardSprite;
        }

        ChildInit();
    }

    protected abstract void ChildInit();

    public void ChangeCard(CardData cardData)
    {
        _currentCard = cardData;
        _currentImage.sprite = _currentCard.CardSprite;
    }

    protected void EmptyCard()
    {
        _currentCard = null;
        _currentImage.sprite = null;
        ChangeAlpha(false);
        _isEmpty = false;
    }

    protected void ChangeAlpha(bool isActive)
    {
        Color color = _currentImage.color;
        color.a = isActive ? 1f : 0f;
        _currentImage.color = color;
    }

    protected void SetShowInfo(bool isStop)
    {
        if(isStop)
        {
            _stopShowInfo = true;
            EventManager.TriggerEvent(Constant.EXIT_CARDPANAL);
        }

        else
        {
            _stopShowInfo = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_isEmpty || _stopShowInfo) return;

        Param param = new Param();
        param.sParam = _currentCard.ID;
        param.iParam = _currentIdx;
        param.vParam = transform.position;

        PEventManager.TriggerEvent(Constant.ENTER_CARDPANAL, param);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isEmpty || _stopShowInfo) return;
        
        EventManager.TriggerEvent(Constant.EXIT_CARDPANAL);
    }
}
