using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;
using Random = UnityEngine.Random;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PoolListSo _initList = null;
    [SerializeField] private CardDataSO _cardDataSO;
    private Transform _playerTrm;
    private UIManager _uiManager;
    private DataManager _dataManager;
    //private bool[] _existCard;
    private int _canCardPickCnt = 12;

    private bool _onUI;

    public bool OnUI
    {
        get => _onUI;
    }

    public bool GameEnd;

    public int CardPickCnt
    {
        get => _canCardPickCnt;
        set => _canCardPickCnt = value;
    }

    public UIManager UI { get => _uiManager; }
    public DataManager Data { get => _dataManager; }

    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            return _playerTrm;
        }
    }

    private void Awake()
    {

        new PoolManager(transform);

        _uiManager = GetComponentInChildren<UIManager>();
        _dataManager = GetComponentInChildren<DataManager>();
        CreatePool();
    }

    private void Start()
    {
        Time.timeScale =1f;
        GameEnd = false;
        _onUI = false;
       // _existCard = Enumerable.Repeat(true, 10).ToArray();
    }

    public void OnTriggerUI(bool onUI)
    {
        Time.timeScale = onUI ? 0f : 1f;
    }

    public void OnTriggrtGameEnd()
    {
        Time.timeScale = 0f;
        GameEnd = true;
    }

    private void CreatePool()
    {
        foreach (PoolingPair pair in _initList.list)
            PoolManager.Inst.CreatePool(pair.prefab, pair.poolCnt);
    }

    public CardData FindCardDataWithID(string cardID)
    {
        return new CardData(_cardDataSO.FindCardData(cardID));
    }

    //public CardData GetWantCardData(int cardNum)
    //{
    //    var cards = _randomCardDeck.FindAll(x => x.CardNum == cardNum);
    //    CardData card = null;

    //    if (cards.Count <= 0)
    //    {
    //        return null;
    //    }

    //    else if (cards.Count == 1)
    //    {
    //        card = cards[0];
    //        _existCard[cardNum - 1] = false;
    //    }
    //    else
    //    {
    //        int idx = Random.Range(0, 2);
    //        card = cards[idx];
    //    }

    //    _randomCardDeck.Remove(card);

    //    return new CardData(card);
    //}

    public CardData GetRandomCardData()
    {
        int idx = Random.Range(0, _cardDataSO.CardDataList.Count);
        CardData randCard = _cardDataSO[idx];

        return randCard;
    }

    //private void ExistCardCheck(int num)
    //{
    //   int cnt = _randomCardDeck.FindAll(x => x.CardNum == num).Count;

    //    if(cnt == 0)
    //    {
    //        _existCard[num - 1] = false;
    //    }
    //}

    //public void AddCardDeck(CardData card)
    //{
    //    int idx = Random.Range(0, _randomCardDeck.Count);

    //    _randomCardDeck.Insert(idx, card);
    //}

    //public bool ExistCard(int cardNum)
    //{
    //    return _existCard[cardNum - 1];
    //}

    public void SetPlayerableCardInfo(string id)
    {
        _dataManager.CurrentPlayer.playerableCardInfoList.Add(id);
    }

    public void SpawnCardGauge(Vector3 pos, float  amout)
    {
        CardGauge gauge = PoolManager.Inst.Pop("CardGauge") as CardGauge;

        gauge.InitGauge(amout);
        gauge.transform.position = pos;
    }

}
