using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBookWeapon : SubWeaponController
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _radius;
    [SerializeField] private int _spawnCnt;
    [SerializeField] private float _spawnTime;
    [SerializeField] private bool _isRight;
    [SerializeField] private Vector2 _offset;

    [Header("·£´ý¿ë")]
    [SerializeField] private bool _isRandomCnt;

    protected override void ChildAttackLoop()
    {
        Book book;
        if(_isRandomCnt)
        {
            _spawnCnt = Random.Range(1, 12);
        }
        for (int i = 0; i < _spawnCnt; i++)
        {
            book = GetWeaponObject() as Book;

            float angle = Mathf.Lerp(0f, 360f, (float)i / _spawnCnt);
            book.InitBook(_rotateSpeed,_radius, angle, _spawnTime, _isRight, GameManager.Inst.PlayerTrm, _offset);
            book.gameObject.SetActive(true);
            book.StartAttack();
        }
    }


}
