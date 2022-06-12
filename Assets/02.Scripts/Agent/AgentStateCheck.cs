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
        set => _isStop = value;
    }

    [SerializeField]
    private bool _isInvincibility = false;
    public bool IsInvincibility
    {
        get => _isInvincibility;
        set
        {
            if (value == true)
                index++;
            else if (value == false && index == 0)
                _isInvincibility = false;
            else if (value == false && index != 0)
                index--;
            else
                index = 0;
        }
    }

    [SerializeField] private int _sheildCnt;

    public bool HaveShield
    {
        get
        {
            return _sheildCnt > 0;
        }

        set
        {
            if (value)
            {
                _sheildCnt++;
            }

            else
            {
                if (_sheildCnt > 0)
                {
                    _sheildCnt--;
                }
            }
        }
    }
}
