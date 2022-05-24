using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UtilDefine;

public class AttackStart : PoolableMono
{
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

    private void Turn(bool dir) // true ¿Þ / false ¿À
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
}
