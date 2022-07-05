using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SunExplosionObject : PoolableMono
{
    private float _explosionRange;
    private float _lifeTime;

    public void StartEffect(float explosionRange, float lifeTime)
    {
        _explosionRange = explosionRange;
        _lifeTime = lifeTime;

        float disappearTime = _lifeTime * 0.3f;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Vector3.one * _explosionRange, _lifeTime - disappearTime).SetEase(Ease.InOutCirc));
        seq.Append(transform.DOScale(Vector3.zero, disappearTime));
    }

    public override void Reset()
    {
        PoolManager.Inst.Push(this);
    }

}
