using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UtilDefine;

public class AttackStart : PoolableMono
{
    [SerializeField]
    private LayerMask _enemyLayer;

    private void OnEnable()
    {
        Vector2 direction = MousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle <= 90f && angle >= -90f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.Rotate(Vector3.forward, 180, Space.Self);
            Turn(false);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Rotate(Vector3.forward, -180, Space.Self);
            Turn(true);
        }
    }

    private void Turn(bool dir) // true 왼 / false 오
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(new Vector3(0, 0, 
            dir == false ? -90 : 90), 0.25f, RotateMode.WorldAxisAdd)
           .SetEase(Ease.OutQuad)).OnComplete(() =>
        {
            PoolManager.inst.Push(this);
        });
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
            hitable.GetHit(damage: 20, damageDealer: gameObject);
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
