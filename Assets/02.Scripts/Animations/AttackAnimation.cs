using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackAnimation : AgentAnimation
{
    [SerializeField] private float _delay = 0.8f;
    public static bool IsAttack = false;

    protected readonly int _atkHashStr = Animator.StringToHash("Atk");

    public UnityEvent OnTriggerAttack;

    public void StartAttack()
    {
        if (IsAttack == true) return;
        IsAttack = true;
        _animator.SetTrigger(_atkHashStr);
        SpawnAttackEffect();
        StartCoroutine(Delay());
    }

    public void SpawnAttackEffect()
    {
        OnTriggerAttack?.Invoke();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        IsAttack = false;
        _agentStateCheck.IsStop = false;
    }

}