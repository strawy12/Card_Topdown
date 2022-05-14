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
        _dataManager = GetComponent<DataManager>();
        ShuffleCardDeck();
        CreatePool();
    }

    private void Start()
    {
        EventManager.StartListening(Constant.TRIGGER_MONSTER_DEAD, PickCardEvent);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMonster();
        }
    }

    private void SpawnMonster()
    {
        Instantiate(_monsterPref);
    }

    public void OnUI()
    {
        Time.timeScale = Time.timeScale == 1f ? 0f : 1f;
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

    public void PickCardEvent()
    {
        //if (Random.Range(0, 100) > 20) return;

        EventManager.TriggerEvent(Constant.TRIGGER_PICK_CARD);

    }

    
}
