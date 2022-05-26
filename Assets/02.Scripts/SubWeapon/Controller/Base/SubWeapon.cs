using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapon : PoolableMono
{
    [SerializeField] protected int _playerOrder;
    protected SpriteRenderer _spriteRenderer;
    protected Collider2D _collider;

    protected float _damage;
    protected float _lifeTime;

    protected bool _attackStart;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();

        ChildAwake();

    }

    protected virtual void ChildAwake()
    {

    }

    public virtual void InitWeapon(float damage, float lifeTime)
    {
        _damage = damage;
        _lifeTime = lifeTime;
    }

    public virtual void StartAttack()
    {
        _attackStart = true;
    }

    /// <summary>
    /// true 일때 해당 order보다 앞, false 일때 뒤
    /// </summary>
    protected void SetOrderInLayer(bool isFront)
    {
        int order = _playerOrder + (isFront ? 1 : -1);
        _spriteRenderer.sortingOrder = order;
    }

    protected virtual void ResetObject()
    {
        _attackStart = false;
        _collider.enabled = false;
        gameObject.SetActive(false);
        PoolManager.inst.Push(this);
    }

    public override void Reset()
    {
    }
}
