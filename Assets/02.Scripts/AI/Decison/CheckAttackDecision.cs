using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttackDecision : AIDecision
{
    private Enemy _enemy;
    protected override void ChildAwake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public override bool MakeDecision()
    {
        return _enemy.IsAttacking;
    }
}
