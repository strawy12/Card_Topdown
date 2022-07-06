using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : MonoBehaviour, IAgent, IHittable
{
    private AgentStateCheck _agentStateCheck;

    [SerializeField]
    private float _initHp = 100f;

    private float _health;
    private float _maxCardGauge = 100;
    private float _cardGuage;

    public UnityEvent OnAddCardGauge;
    private Stack<Action<float, GameObject>> _hitShleidActionStack = new Stack<Action<float, GameObject>>();
    private Stack<Action<int, float>> _ccAtkShleidActionStack = new Stack<Action<int, float>>();

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
    [field: SerializeField]
    public UnityEvent<float, GameObject> OnGetHitDealer { get; set; }


    [field: SerializeField]
    public UnityEvent OnChangeCharacterEvent { get; set; }

    [SerializeField]
    private BarUI _playerHpBar = null;

    [SerializeField]
    private BarUI _cardGaugeBar = null;

    public SkillDataSO SkillData = null;

    [SerializeField]
    private Transform _image = null;

    private void Awake()
    {
        _agentStateCheck = GetComponent<AgentStateCheck>();

        _image = GameObject.Find("UICanvas").GetComponent<Transform>();
        _image = _image.Find("GameOverPanal").GetComponent<Transform>();

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


    public void PushHitShieldStack(Action<float, GameObject> hitAction)
    {
        _hitShleidActionStack.Push(hitAction);
    }
    public void PopHitShieldStack()
    {
        _hitShleidActionStack.Pop();
    }

    public void PushCCAtkShieldStack(Action<int, float> ccAtkAction)
    {
        _ccAtkShleidActionStack.Push(ccAtkAction);
    }
    public void PopCCAtkShieldStack()
    {
        _ccAtkShleidActionStack.Pop();
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_agentStateCheck.IsDead == true) return;
        if (_agentStateCheck.IsInvincibility == true) return;

        if (_hitShleidActionStack.Count != 0)
        {
            Action<float, GameObject> OnHitShield = _hitShleidActionStack.Peek();

            OnHitShield?.Invoke(damage, damageDealer);
            return;
        }

        GetHitDamage(damage);
    }

    public void GetCrowdCtrl(int types, float amount)
    {
        if (_agentStateCheck.IsDead == true) return;
        if (_agentStateCheck.IsInvincibility == true) return;

        if (_ccAtkShleidActionStack.Count != 0)
        {
            var OnHitShield = _ccAtkShleidActionStack.Peek();

            OnHitShield?.Invoke(types, amount);
            return;
        }


        //cc기 적용
    }

    public void GetHitDamage(float damage)
    {
        Health -= damage;
        OnGetHit?.Invoke();
        OnGetHitDealer?.Invoke(damage, gameObject);
        _playerHpBar.GaugeBarGaugeSetting(_health / _initHp);

        if (Health <= 0)
        {
            OnDie?.Invoke();
            _agentStateCheck.IsDead = true;
            BoxCollider2D boxcol2D = GetComponent<BoxCollider2D>();
            boxcol2D.enabled = false;
            AgentInput agentInput = GetComponent<AgentInput>();
            agentInput.enabled = false;
            _image.gameObject.SetActive(true);

        }
    }

    public void CharacterChange()
    {
        if (_agentStateCheck.IsDead == false && _agentStateCheck.IsStop) return;
        OnChangeCharacterEvent?.Invoke();
    }

    private void AddCardGauge(Param param)
    {
        _cardGuage += param.fParam;
        OnAddCardGauge?.Invoke();
        _cardGaugeBar.GaugeBarGaugeSetting(_cardGuage / _maxCardGauge);

        if (_cardGuage >= _maxCardGauge)
        {
            GameManager.Inst.CardPickCnt++;
            EventManager.TriggerEvent(Constant.TRIGGER_PICK_CARD);
            _cardGuage %= _maxCardGauge;
        }

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

                if (gauge != null)
                {
                    gauge.Despawn(transform);
                }

            }
        }
    }

    public void OnDestroy()
    {
        PEventManager.StopListening(Constant.ADD_CARD_GAUGE, AddCardGauge);
    }

}
