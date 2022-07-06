using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using static UtilDefine;
public class MinoAttack : EnemyAttack
{
    public Vector2 attackBoxSize;
    public Transform attackPos;
    public override void Attack(float damage)
    {
        if (!_waitBeforeNextAttack)
        {
            AttackFeedback?.Invoke();
            _isAttacking = true;
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
    public virtual void MeleeAttackCollider() 
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPos.position, attackBoxSize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable.GetHit(_enemy.EnemyData.damage, _enemy.gameObject);
            }
        }
    }
    public IEnumerator EffectCoroutine(Vector3 dir)
    {
        for (int i = 1; i <= 5; i++)
        {
            Earthquake earthquake = PoolManager.Inst.Pop("Earthquake") as Earthquake;
            earthquake.SapwnEffect(_enemy, attackPos.position + dir.normalized * i);
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void SpawnEffect()
    {
        Vector3 dir = PlayerRef.transform.position - transform.position;
        StartCoroutine(EffectCoroutine(dir));

    }
}
