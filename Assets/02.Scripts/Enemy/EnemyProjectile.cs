using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
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
    private EffectAudio _effectAudio;
    private void Awake()
    {
        _playerLayer = LayerMask.NameToLayer("Player");
        _damage = _projectileData.damage;
        _effectAudio = transform.Find("EffectSound").GetComponent<EffectAudio>();
    }

    private void FixedUpdate()
    {
        if (_isChaging) return;

        _timeToLive += Time.deltaTime;
        if(_timeToLive >= _projectileData.lifeTime)
        {
            _isDead = true;
            PoolManager.Inst.Push(this);
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

       PoolManager.Inst.Push(this);    
    }

    private void HitTarget(Collider2D collision)
    {
        IHittable hittable = collision.GetComponent<IHittable>();
        hittable.GetHit(Damage, gameObject);

        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        ParticleScript effect = PoolManager.Inst.Pop(_projectileData.particlePrefab.name) as ParticleScript;
        float _angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        effect.transform.rotation = Quaternion.Euler(0, 0, _angle);
        effect.transform.position = transform.position;
        effect.transform.localScale = new Vector3(_projectileData._explosionRange, _projectileData._explosionRange, _projectileData._explosionRange);

    }
    public void CompleteCharging()
    {
        _isChaging = false;
        _effectAudio.PlaySound();
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
