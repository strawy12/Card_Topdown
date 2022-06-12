using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainAnimalObject : PoolableMono
{
    private SpriteRenderer _spriteRenderer;
    private Transform _targetTrs;

    public Action<Collider2D> OnHitTarget;

    public void Init()
    {

    }

    public void TakeAction()
    {

    }

    public override void Reset()
    {
        PoolManager.Inst.Push(this);
    }
}
