using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono, IHittable, IKnockback
{
    [SerializeField] private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;
    private AgentMove _agentMove;
    private EnemyAttack _enemyAttack;
    public BarUI _hpBar;
    public float Health { get; private set; }
    private WaveController _waveController;
    private AgentStateCheck _agentStateCheck;

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    public bool IsEnemy => true;
    private bool _isStiff = false;
    public bool IsStiff { get => _isStiff; }
    public bool IsAttacking { get => _enemyAttack._isAttacking;}
    public Vector3 HitPoint { get; private set; }
    
    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_isStiff) return;
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
        _hpBar?.GaugeBarGaugeSetting(Health/_enemyData.maxHealth);
        if(!_enemyData.haveSuperAmmor)
            Staff(0.1f);
        if (Health <= 0)
        {
            _agentStateCheck.IsDead = true;
            _agentMove.StopImmediatelly();
            _agentMove.enabled = false;
            OnDie?.Invoke();
            //_waveController.RemainEnemy--;
        }
    }
    private void Awake()
    {
        _agentMove = GetComponent <AgentMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _agentStateCheck = GetComponent<AgentStateCheck>();
        _waveController = GameObject.Find("WaveController").GetComponent<WaveController>();
        _hpBar = transform.Find("HpBar").GetComponent<BarUI>();
    }
    public void EnemyAttack()
    { 
      if (_agentStateCheck.IsDead == false && !_isStiff)
      {
           _enemyAttack.Attack(_enemyData.damage);
      }
    }
    public override void Reset()
    {
        ResetHP();
        _agentStateCheck.IsDead = false;
        _agentMove.enabled = true;
    }
    private void Start()
    {
        ResetHP();
    }

    private void ResetHP()
    {
        if (_hpBar == null) return;
        Health = _enemyData.maxHealth;
        _hpBar.GaugeBarGaugeSetting(Health / _enemyData.maxHealth);
    }

    private void Update()
    { 
        StopDuetoAttack();
    }

    private void StopDuetoAttack()
    {
        if (_enemyAttack._isAttacking)
        {
            _agentMove.StopImmediatelly();
        }
        else if(_agentStateCheck.IsStop)
        {
            _agentMove.EndMoveStop();
        }
    }

    public void Die()
    {
        _agentStateCheck.IsDead = true;
        PoolManager.Inst.Push(this);

        GameManager.Inst.SpawnCardGauge(transform.position, _enemyData.cardGague);
    }

    public void KnockBack(Vector2 dir, float power, float duraction)
    {
        if (!_agentStateCheck.IsDead)
        {
            if(power > _enemyData.knockbackRegist)
            {
                _agentMove.Knockback(dir, power, duraction);
            }
        }
    }

    public void Staff(float duraction)
    {
        if(!_agentStateCheck.IsDead && !_isStiff)
        {
            StartCoroutine(StaffCoroutine(duraction));
        }
    }
    private IEnumerator StaffCoroutine(float duraction)
    {
        _isStiff = true;
        yield return new WaitForSeconds(duraction);
        _isStiff = false;
    }
}
