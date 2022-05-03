using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : AgentAnimation
{
    protected bool isAttack = false;

    protected readonly int _atkHashStr = Animator.StringToHash("Atk");

    public void StartAttack()
    {
        isAttack = true;
        animator.SetTrigger(_atkHashStr);

    }
}
