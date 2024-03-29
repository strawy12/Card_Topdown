using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UtilDefine;

public class AttackStart : PoolableMono
{
    [SerializeField]
    private LayerMask _enemyLayer;

    public void Attack()
    {
        Vector2 direction = MousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle <= 90f && angle >= -90f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Turn(false);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Turn(true);
        }
    }

    private void Turn(bool dir) // true 왼 / false 오
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(new Vector3(0, 0, 
            dir == false ? -135 : 135), 0.25f, RotateMode.WorldAxisAdd)
           .SetEase(Ease.OutQuad)).OnComplete(() =>
        {
            PoolManager.Inst.Push(this);
        });
        CalculateAttack();
    }

    public override void Reset()
    {
        transform.DOKill();
    }

    public void CalculateAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 1f, _enemyLayer);
        foreach (Collider2D enemy in enemys)
        {
            IHittable hitable = enemy.GetComponent<IHittable>();
            // 데미지는 스테이터스에서 받아올거임
            float damage = PlayerStatusManager.Inst.DynamicPlayerStatus.attackDamage;
            hitable.GetHit(damage: damage, damageDealer: gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 1f);
            Gizmos.color = Color.white;
        }
    }
#endif
}
