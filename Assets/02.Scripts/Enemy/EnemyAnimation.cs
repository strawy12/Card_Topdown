using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyAnimation : AgentAnimation
{
    public UnityEvent EndDieAnimation;
    public UnityEvent EndAttackAnimation;
    public UnityEvent HittableAttackAnimation;
    protected readonly int _attackHashStr = Animator.StringToHash("Attack");
    protected readonly int _staffHashStr = Animator.StringToHash("Staff");
    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackHashStr);
    }
    public void PlayHitAnimation()
    {
        _animator.SetTrigger(_staffHashStr);
    }
    public void EndDIeAnim()
    {
        EndDieAnimation?.Invoke();
    }
    public void HittableAttackAnim()
    {
        HittableAttackAnimation?.Invoke();
    }
    public void EndAttackAnim()
    {
        EndAttackAnimation?.Invoke();
    }
}
