using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constant;

public class CardInventoryManager : MonoBehaviour
{
    [SerializeField] private int _initPickCnt = 2;
    private List<CardPanal> _cardPanalList;
    private MountCardPanal[] _canChangeCardPanals;

    private void Awake()
    {
        _cardPanalList = new List<CardPanal>();
        _canChangeCardPanals = new MountCardPanal[2];

    }
    private void Start()
    {
        PEventManager.StartListening(ENTER_MOUNTING_UI, ChangeMountingCard);
        PickInitCard();
    }

    public void AddCardPanalList(CardPanal panal)
    {
        if(panal.IsDeferPanal)
        {
            _cardPanalList.Insert(0, panal);
        }
        else
        {
            _cardPanalList.Add(panal);
        }
    }

    public void FormActivePanal(CardPanal panal)
    {
        if (_canChangeCardPanals.Length == 2)
        {
            _canChangeCardPanals = new MountCardPanal[2];
            _canChangeCardPanals[0] = (MountCardPanal)panal;
        }

        else
        {
            int idx = _canChangeCardPanals.Length;
            _canChangeCardPanals[idx] = (MountCardPanal)panal;
        }
    }
    private void ChangeTwoCardPanal(int idx1, int idx2)
    {

    }

    private void ChangeMountingCard(Param param)
    {
        if (_canChangeCardPanals[0] == null)
        { 
            CardData card = GameManager.Inst.FindCardDataWithID(param.sParam);
            _cardPanalList[2].ChangeCard(card);

            return;
        }

        ActiveChangePanals();
    }

    private void ActiveChangePanals()
    {
        int id1 = _canChangeCardPanals[0].ID;
        int id2 = -1;

        if (_canChangeCardPanals[1] != null)
        {
            id2 = _canChangeCardPanals[1].ID;
        }

        for (int i = 2; i < _cardPanalList.Count; i++)
        {
            CardPanal panal = _cardPanalList[i];

            if (panal.ID == id1 || panal.ID == id2)
            {
                panal.AcitvePanal(true);
            }

            else
            {
                panal.AcitvePanal(false);
            }
        }
    }

    public void AcitveAllCardPanal()
    {
        foreach (CardPanal panal in _cardPanalList)
        {
            panal.AcitvePanal(true);
        }
    }

    private void PickInitCard()
    {
        for (int i = 0; i < _initPickCnt; i++)
        {
            CardData card = GameManager.Inst.GetRandomCardData();
            _cardPanalList[i].ChangeCard(card);
        }
    }
}
