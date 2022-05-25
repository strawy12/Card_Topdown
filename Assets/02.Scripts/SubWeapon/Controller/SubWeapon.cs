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

    }

    protected void SetOrderInLayer(bool isFront)
    {
        int order = _playerOrder + (isFront ? 1 : -1);
        _spriteRenderer.sortingOrder = order;
    }

    public override void Reset()
    {
    }
}
