using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrawPickCardUI : MonoCardUI
{
    [SerializeField] private Image _disappearCardImage;
    [SerializeField] private Text _disappearCardText;

    [SerializeField] private Image _newCardImage;
    [SerializeField] private Text _newCardText;

    [SerializeField] private List<DrawCardSelectPanal> _selectPanalList;


    private CardPanal _currentCardPanal;
    private DrawCardSelectPanal _currentSelectPanal;
    // TODO : 복사 버그 수정  
    private void Awake()
    {
        EventManager.StartListening(Constant.ACTIVE_DRAWPICK_UI, ActiveUI);
        // TODO : UI Panal Stack 구조 구현

        gameObject.SetActive(false);
    }

    public void ActiveUI()
    {
        if (Init() == false) return;

        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.6f).SetUpdate(true).SetEase(Ease.InOutElastic);
        GameManager.Inst.UI.PushPanal(gameObject);
    }

    public void UnActiveUI(System.Action action = null)
    {
        GameManager.Inst.UI.ClosePanal(gameObject, action);
    }
    private bool Init()
    {
        if (InitDisappearCard())
        {
            InitSelectPanal();
            SelectCard(0);

            return true;
        }

        return false;
    }

    private bool InitDisappearCard()
    {
        _currentCardPanal = InventoryManager.GetLastCardPanal();
        CardData cardData = _currentCardPanal.CurrentCard;
        if (cardData == null) return false;

        _disappearCardImage.sprite = cardData.CardSprite;
        _disappearCardText.text = $"{cardData.CardNum}월";

        return true;
    }

    private void InitSelectPanal()
    {
        List<CardPanal> list = InventoryManager.GetDeferCardPanals();
        int cnt = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].CurrentCard != null)
            {
                _selectPanalList[i].InitPanal(list[i].CurrentCard, DrawCardSelectPanal.ECardType.Defer, SelectCard);
                cnt++;
            }
        }

        CardData newCard = GameManager.Inst.GetRandomCardData();
        _selectPanalList[cnt].InitPanal(newCard, DrawCardSelectPanal.ECardType.NewPick, SelectCard);

        for (int i = cnt; i < 2; i++)
        {
            _selectPanalList[i].gameObject.SetActive(false);
        }
    }

    private void SelectCard(int idx)
    {
        Debug.Log(idx);
        if (_selectPanalList[idx].CurrentCard == null)
        {
            return;
        }

        if (_currentSelectPanal != null)
        {
            _currentSelectPanal.interactable = true;
        }

        _newCardImage.sprite = _selectPanalList[idx].CurrentCard.CardSprite;
        _newCardText.text = $"{_selectPanalList[idx].CurrentCard.CardNum}월";

        _selectPanalList[idx].interactable = false;
        _currentSelectPanal = _selectPanalList[idx];
    }

    public void ChangeCard()
    {
        if (_currentSelectPanal == null && _currentCardPanal == null) return;

        InventoryManager.DrawCardMount(_currentSelectPanal.CurrentCard, _currentCardPanal);

        _currentCardPanal = null;
        _currentSelectPanal = null;
        _selectPanalList.ForEach(panal => panal.CurrentCard = null);
        UnActiveUI();
    }
}
