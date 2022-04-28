using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardInventoryManager : MonoBehaviour
{
    [SerializeField] private CardPanal _cardPanalTemp;

    private List<CardPanal> _cardPanalList;

    private void Start()
    {
        _cardPanalList = new List<CardPanal>();
    }

    public void AddCardPanalList(CardPanal panal)
    {
        _cardPanalList.Add(panal);
    }
}
