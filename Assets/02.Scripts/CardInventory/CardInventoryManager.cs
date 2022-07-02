using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constant;

public class CardInventoryManager : MonoSingleton<CardInventoryManager>
{
    [SerializeField] private Button _pickButton;
    [SerializeField] private Text _pickCountText;
    private CanvasGroup _currentCanvasGroup;

    private List<CardPanel> _cardPanelList;

    private bool _isActiveInventory;

    public bool IsActive => _isActiveInventory;


    private void Awake()
    {
        _cardPanelList = new List<CardPanel>();
        _currentCanvasGroup = GetComponent<CanvasGroup>();
    }

    #region 인벤토리 UI 관련
    public void OnActivePanal()
    {
        _isActiveInventory = !_isActiveInventory;

        if (_isActiveInventory)
        {
            gameObject.SetActive(true);
            SetPickEventUI();
            _currentCanvasGroup.interactable = true;
            _currentCanvasGroup.blocksRaycasts = true;
            EventManager.TriggerEvent(Constant.OPEN_INVENTORY);
        }

        _currentCanvasGroup.DOKill();



        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(_currentCanvasGroup.DOFade(_isActiveInventory ? 1f : 0f, 0.5f));

        if (_isActiveInventory == false)
        {
            seq.AppendCallback(CloseInventory);
        }

        else
        {
            GameManager.Inst.UI.ClosePanalAll();
        }

        seq.Play();
        GameManager.Inst.UI.OnUI?.Invoke(_isActiveInventory);
    }

    private void SetPickEventUI()
    {
        _pickButton.interactable = GameManager.Inst.CardPickCnt > 0;
        _pickCountText.text = GameManager.Inst.CardPickCnt.ToString();
    }

    private void CloseInventory()
    {
        if (_isActiveInventory) return;

        gameObject.SetActive(false);
        _currentCanvasGroup.interactable = false;
        _currentCanvasGroup.blocksRaycasts = false;
        GameManager.Inst.UI.ClosePanalAll();

    }

    #endregion

    public void AddCardPanel(CardPanel panel)
    {
        if (panel.Type == ECardPanelType.Own)
        {
            int idx;

            for (idx = 0; idx < _cardPanelList.Count; idx++)
            {
                if (_cardPanelList[idx].Type != ECardPanelType.Own)
                {
                    break;
                }
            }

            _cardPanelList.Insert(idx, panel);
        }
        else
        {
            _cardPanelList.Add(panel);
        }
    }
    public void TriggerPickCard()
    {
        GameManager.Inst.CardPickCnt--;
        SetPickEventUI();
    }
    public void RandomPickCard()
    {
        TriggerPickCard();

        CardData card = GameManager.Inst.GetRandomCardData();

        foreach (var panel in _cardPanelList)
        {
            if (panel.IsEmpty)
            {
                panel.ChangeCard(card, true);
                return;
            }

            else if (panel.Type == ECardPanelType.Own && panel.CurrentCardData.ID.Equals(card.ID))
            {
                panel.ChangeCard(card);
                return;
            }
        }

        _cardPanelList[_cardPanelList.Count - 1].ChangeCard(card, true);
    }

    public void DeleteCard(string panelID)
    {
        CardPanel panel = _cardPanelList.Find(x => x.ID == panelID);

        panel.EmptyCard();
    }

    public void MountingMessage(string panelID, CardData data)
    {
        ButtonStyle btn1 = new ButtonStyle(UtilDefine.EButtonStyle.Okay, () =>
        {
            MountCard(data);
        });

        ButtonStyle btn2 = new ButtonStyle(UtilDefine.EButtonStyle.Cancel, () =>
        {
            ReturnCard(panelID, data);
        });

        GameManager.Inst.UI.TriggerMessage(MESSAGE_MOUNTING, btn1, btn2);
    }

    public void ReturnCard(string panelID, CardData data)
    {
        CardPanel panel = _cardPanelList.Find(x => x.ID == panelID);

        panel.ChangeCard(data, false);
    }

    private void MountCard(CardData data)
    {
        foreach (CardPanel panal in _cardPanelList)
        {
            if (panal.IsEmpty && panal.Type == ECardPanelType.Equip)
            {
                panal.ChangeCard(data);
                return;
            }
        }

        _cardPanelList[_cardPanelList.Count - 1].ChangeCard(data, true);
    }
}
