using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECardType
{
    None,
    Sun,
    Mountain,
    River,
    Rock,
    Cloud,
    Bamboo,
    Pine,
    Turtle,
    Crane,
    Deer
}

[System.Serializable]
public class CardData
{
    [Header("카드 정보")]
    [SerializeField] private string _cardName;

    [Header("카드 데이터")]
    [SerializeField] private ECardType _cardType;
    [SerializeField] private Sprite _cardSprite;

    public string ID { get => _cardType.ToString(); }
    public int CardNum { get => (int)_cardType; }
    public Sprite CardSprite { get => _cardSprite; }


    public CardData(CardData cardData)
    {
        _cardName = cardData._cardName;
        _cardSprite = cardData._cardSprite;
        _cardType = cardData._cardType;
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