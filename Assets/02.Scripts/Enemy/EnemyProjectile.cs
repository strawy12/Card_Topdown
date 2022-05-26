using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : PoolableMono
{
    [SerializeField] protected projectileSO _projectileData;
    protected Collider2D _collider = null;
    protected Rigidbody2D _rigid;
    protected SpriteRenderer _spriteRenderer;
    protected Animator _anim;
    protected float _timeToLive;
    private int _playerLayer;
    private bool _isDead = false;
    private int _damage;
    public int Damage
    {
        get
        {
            if (_damage <= 0)
                _damage = 0;
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    public bool _isChaging =false;
    public projectileSO ProjectileData
    {
        get => _projectileData;
        set
        {
            _projectileData = value;
            if(_rigid == null)
            {
                _rigid = GetComponent<Rigidbody2D>();
            }
            if(_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            _spriteRenderer.sprite = _projectileData.sprite;
            if(_projectileData.material != null)
                _spriteRenderer.material = _projectileData.material;
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
            }
            _anim.runtimeAnimatorController = _projectileData.animatorController;
            if (_collider == null)
            {
                _collider = GetComponent<Collider2D>();
            }
        }
    }

    private void Awake()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
        _damage = _projectileData.damage;
    }

    private void FixedUpdate()
    {
        if (_isChaging) return;

        _timeToLive += Time.deltaTime;
        if(_timeToLive >= _projectileData.lifeTime)
        {
            _isDead = true;
            PoolManager.inst.Push(this);
        }

        if(_rigid != null && _projectileData != null)
        {
            _rigid.MovePosition(transform.position + _projectileData.speed * transform.right * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;
        if(collision.gameObject.layer == _playerLayer)
        {
            HitTarget(collision);
        }
        _isDead = true;
        PoolManager.inst.Push(this);    
    }

    private void HitTarget(Collider2D collision)
    {
        IHittable hittable = collision.GetComponent<IHittable>();
        hittable.GetHit(Damage, gameObject);
    }
    public void CompleteCharging()
    {
        _isChaging = false;
        Debug.Log(_isChaging);
    }
    public override void Reset()
    {
        _timeToLive = 0;
        _isDead = false;
    }

    public void SetPositionAndRotation(Vector2 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }
}
