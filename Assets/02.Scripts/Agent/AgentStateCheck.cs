using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentStateCheck : MonoBehaviour
{
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
}
