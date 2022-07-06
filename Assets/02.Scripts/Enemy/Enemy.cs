using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono, IHittable, IKnockback, IStaff
{
    [SerializeField] private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;
    private AgentMove _agentMove;
    private EnemyAttack _enemyAttack;
    public BarUI _hpBar;
    public float Health { get; private set; }
    private AgentStateCheck _agentStateCheck;

    [field: SerializeField] public UnityEvent OnDieFeedback { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    public bool IsEnemy => true;
    private bool _isStiff = false;
    public bool IsStiff { get => _isStiff; }
    public bool IsAttacking { get => _enemyAttack._isAttacking;}
    private WaveController _waveController; 
    public Vector3 HitPoint { get; private set; }

    public int _waterStack = 0;
    public int WaterStack
    {
        get => _waterStack;
        set => _waterStack = value;
    }
    [SerializeField] private bool _isHitCloud = false;
    public bool IsHitCloud
    {
        get => _isHitCloud;
        set => _isHitCloud = value;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_isStiff) return;
        if (_agentStateCheck.IsDead == true) return;
        Health -= damage;   
        HitPoint = damageDealer.transform.position;
        OnGetHit?.Invoke();
        _hpBar?.GaugeBarGaugeSetting(Health/_enemyData.maxHealth);
        if(!_enemyData.haveSuperAmmor)
        Staff(0.1f);
        if (Health <= 0)
        { 
            _agentMove.StopImmediatelly();
            OnDieFeedback?.Invoke();
            Die();
        }
    }
    private void Awake()
    {
        _waveController = FindObjectOfType<WaveController>();
        _agentMove = GetComponent <AgentMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _agentStateCheck = GetComponent<AgentStateCheck>();
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
        _agentStateCheck.IsStop = false;
        _agentStateCheck.IsDead = false;
        _isStiff = false;
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
        if (_agentStateCheck.IsDead) return;
        _agentStateCheck.IsDead = true;
        _waveController.RemainEnemy--;
        PoolManager.Inst.Push(this);
        GameManager.Inst.SpawnCardGauge(transform.position, _enemyData.cardGague);
    }

    public void CheckWaterStack(float stunTime)
    {
        if (_waterStack >= 3)
        {
            _waterStack = 0;
            StartCoroutine(WaterStun(stunTime));
        }
    }

    IEnumerator WaterStun(float stunTIme)
    {
        _agentStateCheck.IsStop = true;
        yield return new WaitForSeconds(stunTIme);
        _agentStateCheck.IsStop = false;
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
    public void GetCrowdCtrl(int types, float amount)
    {
        return;
    }
}
