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
    public virtual void MeleeAttackCollider() // 박스 형태의 판정을 안사용하는 애들도 있을거 같아 virtual로 구현해놈
    { 
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                //플레이어 맞는 함수 혹은 이벤트
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
