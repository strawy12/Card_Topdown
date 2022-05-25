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
    [SerializeField] private GameObject _monsterPref;
    private Transform _playerTrm;
    private UIManager _uiManager;
    private DataManager _dataManager;
    private List<CardData> _randomCardDeck;
    private bool[] _existCard;

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

        _uiManager = FindObjectOfType<UIManager>();
        _dataManager = GetComponentInChildren<DataManager>();
        ShuffleCardDeck();
        CreatePool();
    }

    private void Start()
    {
        EventManager.StartListening(Constant.TRIGGER_MONSTER_DEAD, PickCardEvent);

        _existCard = Enumerable.Repeat(true, 10).ToArray();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        Instantiate(_monsterPref);
    }

    public void OnUI(bool onUI)
    {
        Time.timeScale = onUI ? 0f : 1f;
    }

    private void CreatePool()
    {
        foreach (PoolingPair pair in _initList.list)
            PoolManager.inst.CreatePool(pair.prefab, pair.poolCnt);
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

    public CardData GetWantCardData(int cardNum)
    {
        var cards = _randomCardDeck.FindAll(x => x.CardNum == cardNum);
        CardData card = null;

        if (cards.Count <= 0)
        {
            return null;
        }

        else if (cards.Count == 1)
        {
            card = cards[0];
            _existCard[cardNum - 1] = false;
        }
        else
        {
            int idx = Random.Range(0, 2);
            card = cards[idx];
        }

        _randomCardDeck.Remove(card);

        return new CardData(card);
    }

    public CardData GetRandomCardData()
    {
        if (_randomCardDeck.Count <= 0)
        {
            Debug.Log("모든 카드를 뽑았습니다.");
            return null;
        }


        CardData randCard = _randomCardDeck[0];
        _randomCardDeck.RemoveAt(0);

        ExistCardCheck(randCard.CardNum);

        return randCard;
    }

    private void ExistCardCheck(int num)
    {
       int cnt = _randomCardDeck.FindAll(x => x.CardNum == num).Count;

        if(cnt == 0)
        {
            _existCard[num - 1] = false;
        }
    }

    public void AddCardDeck(CardData card)
    {
        int idx = Random.Range(0, _randomCardDeck.Count);

        _randomCardDeck.Insert(idx, card);
    }
<<<<<<< HEAD

    public void PickCardEvent()
    {
        //if (Random.Range(0, 100) > 20) return;

        EventManager.TriggerEvent(Constant.TRIGGER_PICK_CARD);

    }

    public bool ExistCard(int cardNum)
    {
        return _existCard[cardNum - 1];
    }

=======
>>>>>>> origin/gaon
}
