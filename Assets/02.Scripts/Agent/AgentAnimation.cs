using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AgentAnimation : MonoBehaviour
{
    protected Animator animator;

    protected readonly int _walkHashStr = Animator.StringToHash("Walk");
    protected readonly int _deathHashStr = Animator.StringToHash("Death");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }

    public void SetWalkAnimation(bool value)
    {
        animator.SetBool(_walkHashStr, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger(_deathHashStr);
    }
}
