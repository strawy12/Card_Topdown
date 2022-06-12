using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BossAttack : EnemyAttack
{
    public Vector2 attackBoxSize;
    public Transform attackPos;
    public override void Attack(float damage)
    {
        if (!_waitBeforeNextAttack)
        {
            _isAttacking = true;
            AttackFeedback?.Invoke();
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
    public IEnumerator EffectCoroutine()
    {
        for(int i = 1; i <= 5; i++)
        {
            Earthquake earthquake = PoolManager.Inst.Pop("Earthquake") as Earthquake;
            earthquake.SapwnEffect(enemy: _enemy,new Vector2(attackPos.position.x + attackPos.parent.transform.right.x * i, attackPos.position.y));
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void SpawnEffect()
    {
        StartCoroutine(EffectCoroutine());
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(attackPos.position, attackBoxSize);
    }
}
