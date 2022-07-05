using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyAIBrain _enemyBrain;
    protected Enemy _enemy;


    protected bool _waitBeforeNextAttack = false;

    public bool WaitingForNextAttack => _waitBeforeNextAttack;

    public bool _isAttacking = false; // 현재 공격중인가?
    public UnityEvent AttackFeedback;
    protected virtual void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _enemyBrain = GetComponent<EnemyAIBrain>();
    }
    protected IEnumerator WaitBeforeAttackCoroutine()
    {
        _waitBeforeNextAttack = true;
        yield return new WaitForSeconds(_enemy.EnemyData.attackDelay);
        _waitBeforeNextAttack = false;
    }
    public void Reset()
    {
        StartCoroutine(WaitBeforeAttackCoroutine());
    }
    public void EndOfAttackAnimation()
    {
        _isAttacking = false;
    }
    public virtual void HitMotionPlay()
    {

    }

    public Transform GetTarget()
    {
        return _enemyBrain.target;
    }
    public abstract void Attack(float damage);

}
