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

    private bool _isDead = false;
    public float Health { get; private set; }

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    public bool IsEnemy => true;

    public Vector3 HitPoint { get; private set; }
    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_isDead) return;
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
        if (Health <= 0)
        {
            _isDead = true;
            _aiMove.StopImmediatelly();
            _aiMove.enabled = false;
            OnDie?.Invoke();
        }
    }
    private void Awake()
    {
        _aiMove = GetComponent <AgentMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }
    public void EnemyAttack()
    { 
            if (!_isDead)
            {
                _enemyAttack.Attack(_enemyData.damage);
            }
    }
    public override void Reset()
    {
        Health = _enemyData.maxHealth;
        _isDead = false;
        _aiMove.enabled = true;
    }
    private void Start()
    {
        Health = _enemyData.maxHealth;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            GetHit(5, GameObject.Find("Player"));
        }
        if (_enemyAttack.IsAttacking)
        {
            _aiMove.StopImmediatelly();
        }
    }
    public void Die()
    {
        Destroy(gameObject);//풀매니저 구현시 변경
    }


}
