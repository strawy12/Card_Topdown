using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAnimation : AgentAnimation
{
    public static bool isAttack = false;

    protected readonly int _atkHashStr = Animator.StringToHash("Atk");

    public void StartAttack()
    {
        _animator.Play("Attack");
    }

    //public void StartAttack()
    //{
    //    if (isAttack == true) return;
    //    isAttack = true;
    //    _animator.Play("Attack1");
    //    StartCoroutine(Delay());
    //}

    //IEnumerator Delay()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    isAttack = false;
    //    AgentMove.isStop = false;
    //    _animator.Play("Idle");
    //}
}