using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static Constant;

public class CardInventoryManager : MonoBehaviour
{
    [SerializeField] private int _initPickCnt = 2;
    [SerializeField] private Button _pickButton = null;
    //[SerializeField] private ChangeCard _changeCardPref;
    private List<CardPanal> _cardPanalList;
    private CanvasGroup _currentPanal;
    private bool _activePanalSelf = false;

    public bool IsActive { get => _activePanalSelf; }
    public int PanalCount { get => _cardPanalList.Count; }
    //private MountCardPanal[] _canChangeCardPanals;
    //private DeferCardPanal _selectDeferCardPanal;

    private void Awake()
    {
        _cardPanalList = new List<CardPanal>();
        //_canChangeCardPanals = new MountCardPanal[2];
    }
    private void Start()
    {
        PEventManager.StartListening(ENTER_MOUNTING_UI, MountingMessage);
        PEventManager.StartListening(TRIGGER_WANT_PICK, WantPickCard);
        EventManager.StartListening(TRIGGER_RANDOM_PICK, () => RandomPickCard(2)); 

        _currentPanal = GetComponent<CanvasGroup>();
        //PickInitCard();
    }

    public void OnActivePanal()
    {
        _activePanalSelf = !_activePanalSelf;

        if (_activePanalSelf)
        {
            gameObject.SetActive(true);
            _pickButton.interactable = GameManager.Inst.CardPickCnt > 0;
            _currentPanal.interactable = true;
            _currentPanal.blocksRaycasts = true;
        }

        _currentPanal.DOKill();

        

        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(_currentPanal.DOFade(_activePanalSelf ? 1f : 0f, 0.5f));

        if(_activePanalSelf == false)
        {
            seq.AppendCallback(CloseInventory);
        }

        else
        {
            GameManager.Inst.UI.ClosePanalAll();
        }

        seq.Play();
        // TODO : �����ؾ��ϴ� �ڵ�
        GameManager.Inst.UI.OnUI?.Invoke(_activePanalSelf);

        //EventManager.TriggerEvent(OPEN_INVENTORY);

    }

    private void CloseInventory()
    {
        if (_activePanalSelf) return;

        gameObject.SetActive(false);
        _currentPanal.interactable = false;
        _currentPanal.blocksRaycasts = false;
        GameManager.Inst.UI.ClosePanalAll();

    }

    public void AddCardPanalList(CardPanal panal)
    {
        if (panal.IsDeferPanal)
        {
            bool isExist = _cardPanalList.Count != 0 && _cardPanalList[0].IsDeferPanal;
            int idx = isExist ? 1 : 0;

            _cardPanalList.Insert(idx, panal);
        }
        else
        {
            _cardPanalList.Add(panal);
        }
    }

    #region �ּ�
    // ������� �Լ�
    //public void FormActivePanal(CardPanal panal)
    //{
    //    if (_canChangeCardPanals[1] != null)
    //    {
    //        _canChangeCardPanals = new MountCardPanal[2];
    //        _canChangeCardPanals[0] = (MountCardPanal)panal;
    //    }

    //    else
    //    {
    //        int idx = _canChangeCardPanals[0] == null ? 0 : 1;
    //        _canChangeCardPanals[idx] = (MountCardPanal)panal;
    //    }
    //}

    // Ŭ���� �ְ� ���´ٸ�
    //private void ChangeTwoCardPanal(Param param)
    //{
    //AcitveAllCardPanal(true);

    //CardPanal panal = System.Array.Find(_canChangeCardPanals, panal => panal.ID == param.iParam);

    //GenerateChangeCard(panal, _selectDeferCardPanal);
    //GenerateChangeCard(_selectDeferCardPanal, panal);
    //}

    //private void GenerateChangeCard(CardPanal currentPanal, CardPanal targetPanal)
    //{
    //    ChangeCard cardObject = Instantiate(_changeCardPref, transform);

    //    currentPanal.ChangeAlpha(0f);
    //    cardObject.Init(currentPanal.transform.position, targetPanal, currentPanal.CurrentCard);

    //}

    //private void ChangeMountingCard(Param param)
    //{
    //    if (_canChangeCardPanals[0] == null)
    //    {
    //        CardData card = GameManager.Inst.FindCardDataWithID(param.sParam);
    //        _cardPanalList[2].ChangeCard(card);
    //        EventManager.TriggerEvent(TRIGGER_MOUNTING_EVENT);

    //        return;
    //    }

    //    EventManager.TriggerEvent(TRIGGER_CHANGE_EVENT);

    //    _selectDeferCardPanal = (DeferCardPanal)_cardPanalList.Find(panal => panal.ID == param.iParam);

    //    ActiveChangePanals();

    //}

    //private void ActiveChangePanals()
    //{
    //    AcitveAllCardPanal(false);

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if(_canChangeCardPanals[i] != null)
    //            _canChangeCardPanals[i].AcitvePanal(true, true);

    //        if(_cardPanalList[i].ID == _selectDeferCardPanal.ID)
    //            _selectDeferCardPanal.AcitvePanal(true, true);

    //    }

    //}
    #endregion

    public void AcitveAllCardPanal(bool isActive)
    {
        foreach (CardPanal panal in _cardPanalList)
        {
            if (panal.IsEmpty) continue;

            panal.AcitvePanal(isActive, false);
        }
    }

    public void TriggerPickCard()
    {
        GameManager.Inst.CardPickCnt--;
        _pickButton.interactable = GameManager.Inst.CardPickCnt > 0;
    }

    private void MountingMessage(Param param)
    {
        ButtonStyle btn1 = new ButtonStyle(UtilDefine.EButtonStyle.Okay, () =>
        {
            MountCard(param);
        });

        ButtonStyle btn2 = new ButtonStyle(UtilDefine.EButtonStyle.Cancel, () =>
        {
            PEventManager.TriggerEvent(RETURN_CARD_EFFECT, param);
        });

        GameManager.Inst.UI.TriggerMessage(MESSAGE_MOUNTING, btn1, btn2);
    }

    private void MountCard(Param param)
    {
        foreach (CardPanal panal in _cardPanalList)
        {
            if (panal.IsEmpty && !panal.IsDeferPanal)
            {
                CardData card = GameManager.Inst.FindCardDataWithID(param.sParam);
                EventManager.TriggerEvent(TRIGGER_MOUNTING_EVENT);
                panal.ChangeCard(card);
                return;
            }
        }

        PEventManager.TriggerEvent(RETURN_CARD_EFFECT, param);
    }

    public void WantPickCard(Param param)
    {
        foreach (CardPanal panal in _cardPanalList)
        {
            if (panal.IsEmpty)
            {
                CardData card = GameManager.Inst.GetWantCardData(param.iParam);
                panal.ChangeCard(card);
                TriggerPickCard();
                return;
            }
        }
    }

    private void RandomPickCard(int pickCnt = 1)
    {
        TriggerPickCard();

        foreach (CardPanal panal in _cardPanalList)
        {
            if (panal.IsEmpty)
            {
                CardData card = GameManager.Inst.GetRandomCardData();
                panal.ChangeCard(card);
                pickCnt--;
            }

            if(pickCnt <= 0)
            {
                return;
            }
        }
    }

    public void DrawCardMount(CardData cardData, CardPanal panal)
    {
        TriggerPickCard();

        GameManager.Inst.AddCardDeck(panal.CurrentCard);
        panal.EmptyCard();
        panal.ChangeCard(cardData);
    }

    public List<CardPanal> GetDeferCardPanals()
    {
        return _cardPanalList.FindAll(panal => panal.IsDeferPanal);
    }

    public CardPanal GetLastCardPanal()
    {
        for (int i = _cardPanalList.Count - 1; i >= 0; i--)
        {
            if (_cardPanalList[i].CurrentCard != null)
            {
                return _cardPanalList[i];
            }
        }

        return _cardPanalList[0];
    }

}
