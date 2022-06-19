using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyRenderer : AgentRenderer
{
    public bool _isReversal = false;
    private EnemyAttack _enemyAttack;

    private void Awake()
    {
        _enemyAttack = transform.parent.GetComponent<EnemyAttack>();
    }

    public override void ChangeFace(Vector2 pointerInput)
    {
        if (_enemyAttack._isAttacking) return;
        Vector3 dir = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, dir);
        if (!_isReversal)
        {
            if (result.z < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (result.z > 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
        else if(_isReversal)
        {
            if (result.z > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else if (result.z < 0)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
