using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Book : SubWeapon
{
    private bool _isRight;
    private float _rotateSpeed;
    private float _maxRadius;
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
            3f));

        seq.Join(transform.DOScale(Vector3.one * 2f, 3f));

        seq.Append(DOTween.To(
            () => _currentRadius,
            value => _currentRadius = value,
            0f,
            _lifeTime).SetDelay(_lifeTime));

        seq.Join(transform.DOScale(Vector3.zero, 3f));

        seq.AppendCallback(() =>
        {
            ResetObject();
        });

        seq.Play();
    }

    protected override void ResetObject()
    {
        _currentRadius = 0f;
        base.ResetObject();
    }

    void FixedUpdate()
    {
        if (!_attackStart) return;
        _currentAngle += (Time.fixedDeltaTime * _rotateSpeed) * (_isRight ? 1f : -1f);
        _currentAngle %= 360f;

        float x = _currentRadius * Mathf.Cos(_currentAngle * Mathf.Deg2Rad);
        float y = _currentRadius * Mathf.Sin(_currentAngle * Mathf.Deg2Rad);

        Debug.Log(_targetTrs.position);
        transform.position = _targetTrs.position + new Vector3(x, y) + _offset;

        if (_currentAngle >= 180f && _currentAngle < 360f)
        {
            SetOrderInLayer(true);
        }

        else
        {
            SetOrderInLayer(false);
        }
    }




    public void InitBook(float speed, float radius, float angle, bool isRight, Transform trs, Vector2 offset)
    {
        _rotateSpeed = speed;
        _maxRadius = radius;
        _currentAngle = angle;
        _isRight = isRight;
        _targetTrs = trs;
        _offset = offset;
    }


}
