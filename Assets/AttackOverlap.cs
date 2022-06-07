using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOverlap : MonoBehaviour
{
    [SerializeField]
    LayerMask _enemyLayer;

    public void CalculateAttack()
    {
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 1f, _enemyLayer);
        foreach (Collider2D enemy in enemys)
        {
            IHittable hitable = enemy.GetComponent<IHittable>();
            // 데미지는 스테이터스에서 받아올거임
            hitable.GetHit(damage: 5, damageDealer: gameObject);
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
