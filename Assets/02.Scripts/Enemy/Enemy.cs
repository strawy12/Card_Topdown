using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{
    [SerializeField] private EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData => _enemyData;
    private AIMove _aiMove;
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
        Health -= damage;   
        HitPoint = damageDealer.transform.position;
        OnGetHit?.Invoke();
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
        _aiMove = GetComponent<AIMove>();
        _enemyAttack = GetComponent<EnemyAttack>();
    }
    public void EnemyAttack()
    { 
            if (!_isDead)
            {
                _enemyAttack.Attack(_enemyData.damage);
            }
    }
    //public override void Reset()
    //{
    //    Health = maxHealth;
    //    _isDead = false;
    //    _agentMovement.enabled = true;
    //} // 풀매니저 구현시 적용 시킬예정
    private void Start()
    {
        Health = _enemyData.maxHealth;
    }
    public void Die()
    {

    }
}
