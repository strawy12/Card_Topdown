using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Book : SubWeapon
{
    private bool _isRight;
    private float _rotateSpeed;
    private float _maxRadius;
    private float _spawnTime;
    private Vector3 _offset;

    private Transform _targetTrs;

    private float _currentAngle = 0f;

    private float _currentRadius;



    public override void StartAttack()
    {
        base.StartAttack();
        transform.localScale = Vector3.zero;

        Sequence seq = DOTween.Sequence();

        seq.Append(DOTween.To(
            () => _currentRadius,
            value => _currentRadius = value,
            _maxRadius,
            _spawnTime));

        seq.Join(transform.DOScale(Vector3.one * 2f, _spawnTime));

        seq.Append(DOTween.To(
            () => _currentRadius,
            value => _currentRadius = value,
            0f,
            _spawnTime).SetDelay(_lifeTime - _spawnTime * 2f));

        seq.Join(transform.DOScale(Vector3.zero, _spawnTime));
    }

    protected override void ResetObject()
    {
        _currentRadius = 0f;
        base.ResetObject();
    }

    void FixedUpdate()
    {
        if (!_attackStart) return;

        Rotate();
        CheckOrderInLayer();
    }


    private void Rotate()
    {
        _currentAngle += (Time.fixedDeltaTime * _rotateSpeed) * (_isRight ? 1f : -1f);
        _currentAngle %= 360f;

        float x = _currentRadius * Mathf.Cos(_currentAngle * Mathf.Deg2Rad);
        float y = _currentRadius * Mathf.Sin(_currentAngle * Mathf.Deg2Rad);

        transform.position = _targetTrs.position + new Vector3(x, y) + _offset;

    }

    private void CheckOrderInLayer()
    {
        if (_currentAngle >= 180f && _currentAngle < 360f)
        {
            SetOrderInLayer(true);
        }

        else
        {
            SetOrderInLayer(false);
        }
    }






    public void InitBook(float speed, float radius, float angle, float spawnTime, bool isRight, Transform trs, Vector2 offset)
    {
        _rotateSpeed = speed;
        _maxRadius = radius;
        _currentAngle = angle;
        _spawnTime = spawnTime;
        _isRight = isRight;
        _targetTrs = trs;
        _offset = offset;
    }


}
