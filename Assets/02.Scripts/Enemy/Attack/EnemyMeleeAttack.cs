using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public Vector2 attackBoxSize;
    public Transform pos;
    public override void Attack(float damage)
    {
        if (!_waitBeforeNextAttack)
        {
            _isAttacking = true;
            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
    public virtual void MeleeAttackCollider() // �ڽ� ������ ������ �Ȼ���ϴ� �ֵ鵵 ������ ���� virtual�� �����س�
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, attackBoxSize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable.GetHit(_enemy.EnemyData.damage, _enemy.gameObject);
            }
        }
    }
}
