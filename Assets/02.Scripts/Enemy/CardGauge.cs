using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGauge : PoolableMono
{
    private float _amout;
    public float Amount
    {
        get => _amout;
    }

    private Transform _targetTrs;
    private bool _triggerDespawn;
    [SerializeField] private float _speed;

    public void InitGauge(float amout)
    {
        _amout = amout; 
    }

    public void LateUpdate()
    {
        if (!_triggerDespawn) return;

        if(_targetTrs == null)
        {
            Release();
        }

        Vector2 targetDir = _targetTrs.position - transform.position;

        transform.Translate(targetDir.normalized * _speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _targetTrs.position) <= 0.5f)
        {
            Release();
        }
    }

    public void Despawn(Transform trs)
    {
        if (_triggerDespawn) return;

        _targetTrs = trs;
        _triggerDespawn = true;
    }

    private void Release()
    {
        Param p = new Param();
        p.fParam = _amout;
        PEventManager.TriggerEvent(Constant.ADD_CARD_GAUGE, p);

        _triggerDespawn = false;
        _amout = 0f;
        PoolManager.Inst.Push(this);
    }

    public override void Reset()
    {
        _amout = 0f;

    }
}
