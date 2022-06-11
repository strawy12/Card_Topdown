using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono, IHittable
{
    [SerializeField] private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;
    private AgentMove _aiMove;
    private EnemyAttack _enemyAttack;
    public BarUI _hpBar;
    public float Health { get; private set; }
    private WaveController _waveController;
    private AgentStateCheck _agentStateCheck;

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    public bool IsEnemy => true;

    public Vector3 HitPoint { get; private set; }

    public void GetHit(float damage, GameObject damageDealer)
    {
      
        if (_agentStateCheck.IsDead == true) return;
        float critical = Random.value;
        bool isCritical = false;

        if(critical <= 0.5f) // 플레이어가 가진 크리티컬 확률 값으로 변경 예정
        {
            float ratio = 1.5f; // 플레이어가 가진 크리티컬 추가 데미지로 변경 예정
            damage = Mathf.CeilToInt((float)damage * ratio);
            isCritical = true;
        }
        Health -= damage;   
        HitPoint = damageDealer.transform.position;
        OnGetHit?.Invoke();
        //DamagePopup popup = Instantiate(new DamagePopup());
        //popup.Setup(damage, transform.position + new Vector3(0, 0.5f, 0), isCritical);
        _hpBar.GaugeBarGaugeSetting(Health/_enemyData.maxHealth);
        if (Health <= 0)
        {
            _agentStateCheck.IsDead = true;
            _aiMove.StopImmediatelly();
            _aiMove.enabled = false;
            OnDie?.Invoke();
            _waveController.RemainEnemy--;
        }
    }
    private void Awake()
    {
        _aiMove = GetComponent <AgentMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _agentStateCheck = GetComponent<AgentStateCheck>();
        _waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        _hpBar = transform.Find("HpBar").GetComponent<BarUI>();

    }
    public void EnemyAttack()
    { 
      if (_agentStateCheck.IsDead == false)
      {
           _enemyAttack.Attack(_enemyData.damage);
      }
    }
    public override void Reset()
    {
        ResetHP();
        _agentStateCheck.IsDead = false;
        _aiMove.enabled = true;
    }
    private void Start()
    {
        ResetHP();
    }

    private void ResetHP()
    {
        Health = _enemyData.maxHealth;
        _hpBar.GaugeBarGaugeSetting(Health / _enemyData.maxHealth);
    }

    private void Update()
    { 
        StopDuetoAttack();
    }

    private void StopDuetoAttack()
    {
        if (_enemyAttack.IsAttacking)
        {
            _aiMove.StopImmediatelly();
            _aiMove.EndMoveStop();
        }
    }

    public void Die()
    {
        PoolManager.Inst.Push(this);

        GameManager.Inst.SpawnCardGauge(transform.position, _enemyData.cardGague);
    }

    public void GetCrowdCtrl(ECrowdControlType type, float amount)
    {
        throw new System.NotImplementedException();
    }
}
