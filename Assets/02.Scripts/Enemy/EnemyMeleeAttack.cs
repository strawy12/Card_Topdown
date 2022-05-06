using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public Vector2 boxsize;
    public Transform pos;
    public override void Attack(float damage)
    {
        if (!_waitBeforeNextAttack)
        {
            IHittable hitable = GetTarget().GetComponent<IHittable>();
            _isAttacking = true;
            hitable?.GetHit(damage: damage, damageDealer: gameObject);
            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
    public virtual void MeleeAttackCollider() // �ڽ� ������ ������ �Ȼ���ϴ� �ֵ鵵 ������ ���� virtual�� �����س�
    { 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                //�÷��̾� �´� �Լ� Ȥ�� �̺�Ʈ
                Debug.Log("Attack success");
            }
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(pos.position, boxsize);
    }

}
