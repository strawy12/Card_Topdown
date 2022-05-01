using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentComboAttackAnimation : AgentAnimation
{
    public static AgentComboAttackAnimation instance;

    protected readonly int attack1HashString = Animator.StringToHash("Attack1");
    protected readonly int attack2HashString = Animator.StringToHash("Attack2");
    protected readonly int attack3HashString = Animator.StringToHash("Attack3");

    public bool isAttacking = false;

    protected override void ChildAwake()
    {
        instance = this;
    }

    private void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
        }
    }
}
