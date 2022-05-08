using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private CardDataSO _cardDataSO;
    private UIManager _uiManager;
    private DataManager _dataManager;
    private List<CardData> _randomCardDeck;

    public UIManager UI { get => _uiManager; }
    public DataManager Data { get => _dataManager; }

    private void Awake()
    {
        _uiManager = GetComponent<UIManager>();
        _dataManager = GetComponent<DataManager>();
        ShuffleCardDeck();
    }


    private void ShuffleCardDeck()
    {
        _randomCardDeck = _cardDataSO.CardDataList.ToList();

        int idx1, idx2;
        CardData temp;

        int maxIdx = _randomCardDeck.Count;

        for (int i = 0; i < 100; i++)
        {
            idx1 = Random.Range(0, maxIdx);
            idx2 = Random.Range(0, maxIdx);

            temp = _randomCardDeck[idx1];
            _randomCardDeck[idx1] = _randomCardDeck[idx2];
            _randomCardDeck[idx2] = temp;
        }

    }

    public CardData FindCardDataWithID(string cardID)
    {
        return new CardData(_cardDataSO.FindCardData(cardID));
    }

    public CardData GetCardData(int idx)
    {
        return new CardData(_cardDataSO[idx]);
    }

    public CardData GetRandomCardData()
    {
        if (_randomCardDeck.Count <= 0)
        {
            Debug.Log("모든 카드를 뽑았습니다.");
            return null;
        }

        int idx = Random.Range(0, _randomCardDeck.Count);

        CardData randCard = _randomCardDeck[idx];
        _randomCardDeck.RemoveAt(idx);
        return randCard;
    }

    public void AddCardDeck(CardData card)
    {
        int idx = Random.Range(0, _randomCardDeck.Count);

        _randomCardDeck.Insert(idx, card);
    }

    private Transform _playerTrm;

    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            return _playerTrm;
        }
    }
}
