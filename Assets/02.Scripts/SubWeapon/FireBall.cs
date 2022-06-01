using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireBall : SubWeapon
{
    private float _angle;
    private float _speed;
    private float _explosionRange;
    private int _throughCnt;

    [SerializeField] private GameObject _explosionEffect;

    private void Start()
    {
        _attackStart = true;
        StartAttack();
    }

    private void FixedUpdate()
    {
        if (!_attackStart) return;

        transform.Translate(Vector2.right * _speed * Time.fixedDeltaTime);
    }

    public override void StartAttack()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        base.StartAttack();
        SetOrderInLayer(true);
        Invoke("ResetObject", _lifeTime);
    }

    public void InitFireBall(Vector2 direction, float speed, float explosionRange, int throughCnt)
    {
        _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _explosionRange = explosionRange;
        _speed = speed;
        _throughCnt = throughCnt;

        _attackStart = false;
    }

    protected override void TriggerEnter(Collider2D col)
    {
        base.TriggerEnter(col);

        ParticleScript effect = PoolManager.Inst.Pop(_explosionEffect.name) as ParticleScript;
        effect.transform.localScale = new Vector3(_explosionRange, _explosionRange, _explosionRange);

        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _explosionRange);

        foreach (var c in cols)
        {
            // 대충 공격
        }
        _throughCnt--;

        if (_throughCnt <= 0)
        {
            ResetObject();
        }
    }
}
