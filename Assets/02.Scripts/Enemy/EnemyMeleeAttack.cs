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

            hitable?.GetHit(damage: damage, damageDealer: gameObject);
            AttackFeedback?.Invoke();
            MeleeAttackCollider();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
    public void MeleeAttackCollider()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pos.position, boxsize, 0);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
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
