using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateCheck : MonoBehaviour
{
    int index = 0;

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
            if (value == true && index == 0)
            {
                _isStop = true;
                index++;
            }
            else if (value == true && index >= 1)
                index++;
            else if (value == false && index == 1)
            {
                _isStop = false;
                index--;
            }
            else if (value == false && index > 1)
                index--;
        }
    }

    [SerializeField]
    private bool _isInvincibility = false;
    public bool IsInvincibility
    {
        get => _isInvincibility;
        set
        {
            if (value == true && index == 0)
            {
                _isInvincibility = true;
                index++;
            }
            else if (value == true && index >= 1)
                index++;
            else if (value == false && index == 1)
            {
                _isInvincibility = false;
                index--;
            }
            else if (value == false && index > 1)
                index--;
        }
    }

    [SerializeField]
    private bool _isAttack = false;
    public bool IsAttack
    {
        get => _isAttack;
        set
        {
            if (value == true && index == 0)
            {
                _isAttack = true;
                index++;
            }
            else if (value == true && index >= 1)
                index++;
            else if (value == false && index == 1)
            {
                _isAttack = false;
                index--;
            }
            else if (value == false && index > 1)
                index--;
        }
    }
}
