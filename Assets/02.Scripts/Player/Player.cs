using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;

public class Player : MonoBehaviour, IAgent, IHittable
{
    private AgentStateCheck _agentStateCheck;

    [SerializeField]
    private float _initHp = 100f;

    private float _health;
    private float _cardGuage;

    public UnityEvent OnAddCardGauge;

    public float Health
    {
        get => _health;
        set
        {
            //_health = Mathf.Clamp(value, 0, _agentStatus.maxHp);
            _health = Mathf.Clamp(value, 0, _initHp);
        }
    }

    public float CardGauge
    {
        get => _cardGuage;
    }


    public bool IsEnemy => false;
    public Vector3 HitPoint { get; private set; }
    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [SerializeField]
    private BarUI _playerHpBar = null;

    [SerializeField]
    private BarUI _cardGaugeBar = null;

    private void Awake()
    {
        _agentStateCheck = GetComponent<AgentStateCheck>();

        if (_playerHpBar == null)
        {
            _playerHpBar = transform.Find("HpBar").GetComponent<BarUI>();
        }
    }

    private void Start()
    {
        _health = _initHp;
        //Health = _agentStatus.maxHp;

        PEventManager.StartListening(Constant.ADD_CARD_GAUGE, AddCardGauge);
    }

    private void Update()
    {
        CardGaugeCheck();
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_agentStateCheck.IsDead == true) return;
        if (_agentStateCheck.IsInvincibility == true) return;


        Health -= damage;
        OnGetHit?.Invoke();
        _playerHpBar.GaugeBarGaugeSetting(_health / _initHp);

        if (Health <= 0)
        {
            OnDie?.Invoke();
            _agentStateCheck.IsDead = true;
        }

    }

    public void CharacterChange()
    {

    }

    private void AddCardGauge(Param param)
    {
        _cardGuage += param.fParam;
        OnAddCardGauge?.Invoke();
        _cardGaugeBar.GaugeBarGaugeSetting(_cardGuage / Constant.MAX_CARDGAUGE);

        if (_cardGuage >= Constant.MAX_CARDGAUGE)
        {
            GameManager.Inst.CardPickCnt++;
            EventManager.TriggerEvent(Constant.TRIGGER_PICK_CARD);
        }

        _cardGuage %= Constant.MAX_CARDGAUGE;
    }

    //변경할 예정(위치, 코드내용 등등)

    [SerializeField] private float _overapDistance = 3f;
    public void CardGaugeCheck()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _overapDistance, 1 << 6);

        foreach (var col in cols)
        {
            if (col.CompareTag("CardGauge"))
            {
                CardGauge gauge = col.GetComponent<CardGauge>();

                if(gauge != null)
                {
                    gauge.Despawn(transform);
                }

            }
        }
    }

}
