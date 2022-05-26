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
        _animator.SetTrigger(_atkHashStr);
        StartCoroutine(Delay());
    }

    //public void StartAttack()
    //{
    //    if (isAttack == true) return;
    //    isAttack = true;
    //    _animator.Play("Attack1");
    //    StartCoroutine(Delay());
    //}

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.3f);
        isAttack = false;
        _agentStateCheck.IsStop = false;
    }

    // 임시용으로 만들어둔 코드 추후에 스크립트 나눠서 짜야함

    public void AttackStartEvent()
    {
        EventManager.TriggerEvent(Constant.PLAYER_ATTACK_START);
    }
    public void AttackEndEvent()
    {
        EventManager.TriggerEvent(Constant.PLAYER_ATTACK_END);
    }
}