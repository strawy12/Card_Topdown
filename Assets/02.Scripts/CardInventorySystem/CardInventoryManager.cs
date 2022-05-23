using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static Constant;

public class CardInventoryManager : MonoBehaviour
{
    [SerializeField] private int _initPickCnt = 2;
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
        if(!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        _currentPanal.DOKill();

        _activePanalSelf = !_activePanalSelf;
        

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
        // TODO : 변경해야하는 코드
        GameManager.Inst.UI.OnUI?.Invoke(_activePanalSelf);

        //EventManager.TriggerEvent(OPEN_INVENTORY);

    }

    private void CloseInventory()
    {
        if (_activePanalSelf) return;

        gameObject.SetActive(false);
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

    #region 주석
    // 쓸모없는 함수
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

    // 클릭한 애가 들어온다면
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
                return;
            }
        }
    }

    private void RandomPickCard(int pickCnt = 1)
    {
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
        for(int i = 0; i < _cardPanalList.Count; i++)
        {
            if(_cardPanalList[i].CurrentCard == null)
            {
                return _cardPanalList[i - 1 < 0 ? 0 : i - 1];
            }
        }

        return _cardPanalList[_cardPanalList.Count - 1];
    }

}
