using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(float damage)
    {
        if (!_waitBeforeNextAttack)
        {
            IHittable hitable = GetTarget().GetComponent<IHittable>();

            hitable?.GetHit(damage: damage, damageDealer: gameObject);
            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
}
