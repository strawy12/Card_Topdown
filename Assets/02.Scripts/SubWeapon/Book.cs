using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Book : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private SubWeaponSO _weaponData;

    [SerializeField] private float _currentAngle = 0f;

    [SerializeField] private bool _isRight;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _maxRadius;
    [SerializeField] private int _playerOrder;
    [SerializeField] private Vector3 _offset;

    private float _currentRadius;
    private bool _startAttack;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartAttack();
    }

    public void StartAttack()
    {
        if (_startAttack) return;

        _spriteRenderer.enabled = true;
         _startAttack = true;
        _currentAngle = 0f;
        _currentRadius = 0;
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
            3f).SetDelay(5f));

        seq.Join(transform.DOScale(Vector3.zero, 3f));

        seq.AppendCallback(() =>
        {
            _spriteRenderer.enabled = false;
            _startAttack = false;
        });

        seq.Play();
    }

    void FixedUpdate()
    {
        if (!_startAttack) return;
        _currentAngle += (Time.fixedDeltaTime * _rotateSpeed) * (_isRight ? 1f : -1f);
        _currentAngle %= 360f;

       

        float x = _currentRadius * Mathf.Cos(_currentAngle * Mathf.Deg2Rad);
        float y = _currentRadius * Mathf.Sin(_currentAngle * Mathf.Deg2Rad);

        transform.position = GameManager.Inst.PlayerTrm.position + new Vector3(x, y) + _offset;

        if(_currentAngle >= 180f && _currentAngle < 360f)
        {
            SetOrderInLayer(true);
        }

        else
        {
            SetOrderInLayer(false);
        }
    }

    private void SetOrderInLayer(bool isFront)
    {
        int order = _playerOrder + (isFront ? 1 : -1);
        _spriteRenderer.sortingOrder = order;
    }
}
