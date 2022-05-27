using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentFireBallWeapon : AgentSubWeapon
{
    [SerializeField] private float _speed;
    [SerializeField] private float _explosionRange;
    [SerializeField] private int _throughCnt;

    private Vector2 _targetDir;

    [SerializeField] private bool _isRandomCnt;

    protected override void ChildAttackLoop()
    {
        if (_isRandomCnt)
        {
            _targetDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        FireBall fireball = GetWeaponObject() as FireBall;

        fireball.InitFireBall(_targetDir, _speed, _explosionRange, _throughCnt);
        fireball.transform.position = transform.position + (Vector3.up * 0.5f);
        fireball.StartAttack();
    }

    public void SetTargetDir(Vector2 mousePos)
    {
        _targetDir = mousePos - (Vector2)transform.position;
    }
}
