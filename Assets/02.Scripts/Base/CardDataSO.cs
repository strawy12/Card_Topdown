using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    [Header("카드 정보")]
    [SerializeField] private string _cardName;

    [Header("카드 데이터")]
    [SerializeField] private string _cardID;
    [SerializeField] private int _cardNum;
    [SerializeField] private bool _isLight;
    [SerializeField] private Sprite _cardSprite;

    public string ID { get => _cardID; set => _cardID = value; }
    public int CardNum { get => _cardNum; }
    public Sprite CardSprite { get => _cardSprite; }
    public bool IsLight { get => _isLight; }


    public CardData(CardData cardData)
    {
        _cardID = cardData._cardID;
        _cardName = cardData._cardName;
        _cardSprite = cardData._cardSprite;
        _cardNum = cardData._cardNum;
    }
}

[CreateAssetMenu(fileName = "CardDataSO", menuName = "SO/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    [SerializeField] private List<CardData> _cardDataList;

    public CardData this[int idx]
    {
        get => _cardDataList[idx];
    }

    public List<CardData> CardDataList
    {
        get => _cardDataList;
    }

    public CardData FindCardData(string cardID)
    {
        return _cardDataList.Find(card => card.ID == cardID);
    }
}