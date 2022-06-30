using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateCheck : MonoBehaviour
{
    int stopIndex = 0;
    int invincibilityIndex = 0;
    int attackIndex = 0;

    [SerializeField]
    private bool _isDead = false;
    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

    [SerializeField]
    private bool _isDashing = false;
    public bool IsDashing
    {
        get => _isDashing;
        set => _isDashing = value;
    }

    [SerializeField]
    private bool _isStop = false;
    public bool IsStop
    {
        get => _isStop;
        set
        {
            if (value == true && stopIndex == 0)
            {
                _isStop = true;
                stopIndex++;
            }
            else if (value == true && stopIndex >= 1)
                stopIndex++;
            else if (value == false && stopIndex == 1)
            {
                _isStop = false;
                stopIndex--;
            }
            else if (value == false && stopIndex > 1)
                stopIndex--;
        }
    }

    [SerializeField]
    private bool _isInvincibility = false;
    public bool IsInvincibility
    {
        get => _isInvincibility;
        set
        {
            if (value == true && invincibilityIndex == 0)
            {
                _isInvincibility = true;
                invincibilityIndex++;
            }
            else if (value == true && invincibilityIndex >= 1)
                invincibilityIndex++;
            else if (value == false && invincibilityIndex == 1)
            {
                _isInvincibility = false;
                invincibilityIndex--;
            }
            else if (value == false && invincibilityIndex > 1)
                invincibilityIndex--;
        }
    }

    [SerializeField]
    private bool _isAttack = false;
    public bool IsAttack
    {
        get => _isAttack;
        set
        {
            if (value == true && attackIndex == 0)
            {
                _isAttack = true;
                attackIndex++;
            }
            else if (value == true && attackIndex >= 1)
                attackIndex++;
            else if (value == false && attackIndex == 1)
            {
                _isAttack = false;
                attackIndex--;
            }
            else if (value == false && attackIndex > 1)
                attackIndex--;
        }
    }

    public void StateReset()
    {
        stopIndex = 0;
        invincibilityIndex = 0;
        attackIndex = 0;

        _isAttack = false;
        _isDashing = false;
        _isDead = false;
        _isInvincibility = false;
        _isStop = false;
    }
}
