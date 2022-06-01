using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

public class MagicWand : SubWeapon
{
    private float _moveSpeed;
    private Vector3 _originPos;

    private float _distance; // 적과의 거리
    private Vector2 _moveDir; // 움직일 방향
    private Collider2D nearEnemy; // 가장 가까운 적

    public override void StartAttack()
    {
        _originPos = transform.position;
        base.StartAttack();
        FindEnemy();
        SetOrderInLayer(false);
        StartCoroutine(DelayOrderLayer());
        Invoke(nameof(ResetObject), _lifeTime);
    }

    private void FindEnemy()
    {
        // 적 방향 구하기
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, 5f, _targetLayer);
        if (enemys.Length <= 0) // 아무것도 안들어왔네 범위안에 없음
        {
             Debug.Log("안들어옴 ");
            // 무작위 방향으로 발사
            _moveDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
            transform.rotation = Quaternion.LookRotation(_moveDir.normalized);
        }
        else // 뭐라도 들어왔으니 확인
        {
            Debug.Log("들어옴 ");
            foreach(Collider2D enemy in enemys)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (_distance == 0)
                    nearEnemy = enemy;
                if (_distance >= distance)
                    nearEnemy = enemy;
            }
            _moveDir = (Vector2)nearEnemy.transform.position - (Vector2)transform.position;
        }
        _moveDir = _moveDir.normalized;
        transform.up = _moveDir;
    }

    public void FixedUpdate()
    {
        if (!_attackStart) return;
        // 자기 방향으로 움직이기
        transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime, Space.Self);
    }

    private IEnumerator DelayOrderLayer()
    {
        yield return new WaitForSeconds(Mathf.Lerp(0f, _lifeTime, 0.4f));
        SetOrderInLayer(true);
    }

    public void InitMagicWand(float moveSpeed)
    {
        _moveSpeed = moveSpeed;

        _attackStart = false;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 5f);
            Gizmos.color = Color.white;
        }
    }
#endif
}
