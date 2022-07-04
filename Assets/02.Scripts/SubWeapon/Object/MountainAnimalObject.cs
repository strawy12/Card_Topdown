using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class MountainAnimalObject : PoolableMono
{
    [SerializeField] private Sprite[] _animalSprites;

    private SpriteRenderer _spriteRenderer;
    private Transform _targetTrs;

    public Action<Collider2D> OnHitTarget;

    private float _moveSpeed;
    private float _rotateSpeed;
    private float _lifeTime;
    [SerializeField] private Vector2 _offset;
    private float _detactRange;
    private float _degreeAngle;
    private bool _isTargeting;
    private bool _isHitDelay;
    private bool _isReturning;
    private bool _isDead;

    private Vector2 _returnPoint;

    public void Init(float moveSpeed, float rotateSpeedFactor, float detactRange, float lifeTime)
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _spriteRenderer.color = Color.white;
        transform.localScale = Vector3.one;

        _detactRange = detactRange;
        _moveSpeed = moveSpeed;
        _rotateSpeed = _moveSpeed * rotateSpeedFactor;
        _lifeTime = lifeTime;

        _targetTrs = UtilDefine.PlayerRef.transform;

        _isTargeting = false;

        _spriteRenderer.sprite = _animalSprites[Random.Range(0, 3)];
        gameObject.SetActive(true);
        StartCoroutine(DelayLifeTime());
    }

    private IEnumerator DelayLifeTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        _isDead = true;
        transform.DOMove(UtilDefine.PlayerRef.transform.position, 1f);
        _spriteRenderer.DOFade(0f, 1.5f);
        transform.DOScale(Vector3.zero, 1f);
    }

    public void Update()
    {
        if (_isDead) return;

        if (_isTargeting)
        {
            Debug.Log("dd)");
            Vector2 dir = Vector2.zero;
            dir = _targetTrs.position - transform.position;
            dir.Normalize();
            transform.Translate(dir * _moveSpeed * Time.deltaTime);
        }

        else
        {
            _degreeAngle += Time.deltaTime * _rotateSpeed;

            Vector3 point;
            if (_isReturning)
            {
                point = Vector3.Lerp(_returnPoint, _targetTrs.position, Time.deltaTime * _moveSpeed);
                _returnPoint = point;
            }

            else
            {
                point = _targetTrs.position;
            }


            float x = Mathf.Cos(_degreeAngle * Mathf.Deg2Rad) + point.x;
            float y = Mathf.Sin(_degreeAngle * Mathf.Deg2Rad) + point.y;

            Vector3 pos = new Vector3(x, y);


            transform.position = pos;

            if (_isReturning)
            {
                if (Vector2.Distance(transform.position, _targetTrs.position) <= 0.5f)
                {
                    _isReturning = false;
                    _isHitDelay = false;
                }
            }

        }

    }

    public void FixedUpdate()
    {
        if (_isDead) return;
        if (_isTargeting) return;

        if (_isHitDelay)
        {
            _targetTrs = UtilDefine.PlayerRef.transform;
            return;
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(UtilDefine.PlayerRef.transform.position, _detactRange);
        Collider2D target = null;
        float minDistance = 999f;
        foreach (var hit in hits)
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(hit.transform.position, transform.position);
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    target = hit;
                }
            }
        }

        if (target != null)
        {
            _targetTrs = target.transform;
            _isTargeting = true;
        }

        else
        {
            _targetTrs = UtilDefine.PlayerRef.transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;
        if (_isHitDelay) return;
        _isReturning = true;
        _isHitDelay = true;
        _isTargeting = false;
        _returnPoint = transform.position;
        OnHitTarget?.Invoke(collision);
    }

    public override void Reset()
    {
        if (_spriteRenderer != null)
            _spriteRenderer.color = Color.white;

        transform.localScale = Vector3.one;
        _targetTrs = null;
        _isTargeting = false;
        _isDead = false;
    }
}
