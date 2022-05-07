using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAnimation : AgentAnimation
{
    protected readonly int _attackHashStr = Animator.StringToHash("Attack");
    public void StartAttackAnimation()
    {
        _animator.SetTrigger(_attackHashStr);
    }
  
}
