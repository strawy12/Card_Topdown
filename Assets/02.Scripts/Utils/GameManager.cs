using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private CardDataSO _cardDataSO;
    
    public CardData FindCardDataWithID(string cardID)
    {
        return new CardData(_cardDataSO.FindCardData(cardID));
    }

    public CardData GetCardData(int idx)
    {
        return new CardData(_cardDataSO[idx]);
    }
}
