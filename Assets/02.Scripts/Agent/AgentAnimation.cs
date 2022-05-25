using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    protected Animator _animator;

    protected readonly int _runHashStr = Animator.StringToHash("Run");
    protected readonly int _deathHashStr = Animator.StringToHash("Death");

    protected AgentStateCheck _agentStateCheck = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _agentStateCheck = GetComponentInParent<AgentStateCheck>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }

    public void SetRunAnimation(bool value)
    {
        _animator.SetBool(_runHashStr, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetRunAnimation(velocity > 0);
    }

    public void PlayDeathAnimation()
    {
        _animator.SetTrigger(_deathHashStr);
    }
}
