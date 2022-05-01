using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    public Animator animator;

    protected readonly int walkingHashString = Animator.StringToHash("Walk");

    protected readonly int deathHashString = Animator.StringToHash("Death");

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
        animator.SetBool(walkingHashString, value);
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }
}
