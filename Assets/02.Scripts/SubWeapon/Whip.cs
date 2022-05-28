using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class Whip : SubWeapon
{
    [SerializeField]
    private LayerMask _enemyLayer;
    private Vector3 _originPos;
    
    private bool _isTurn;

    public override void StartAttack()
    {
        _originPos = transform.position;
        base.StartAttack();
        LeftRightCheck();
        SetOrderInLayer(false);
        StartCoroutine(DelayOrderLayer());
        Invoke(nameof(ResetObject), _lifeTime);
    }

    // 플레이어 기준 어디로 채찍을 휘둘러야하는지 찾음
    private void LeftRightCheck()
    {
        float pScale = PlayerTrm.localScale.x;
        if (_isTurn == true)
            transform.localScale = new Vector3(pScale, 1, 1);
        else
            transform.localScale = new Vector3(-1 * pScale, 1, 1);
    }

    public void FixedUpdate()
    {
        if (!_attackStart) return;
        // 자기 방향으로 움직이기
    }

    private IEnumerator DelayOrderLayer()
    {
        yield return new WaitForSeconds(Mathf.Lerp(0f, _lifeTime, 0.4f));
        SetOrderInLayer(true);
    }

    public void InitWhip(bool isTurn)
    {
        _isTurn = isTurn;

        _attackStart = false;
    }
}
