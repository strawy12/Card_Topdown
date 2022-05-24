using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;

public class Player : MonoBehaviour, IAgent, IHittable
{
    private AgentStateCheck _agentStateCheck;

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            //_health = Mathf.Clamp(value, 0, _agentStatus.maxHp);
            _health = Mathf.Clamp(value, 0, 100);
        }
    }

    public bool IsEnemy => false;
    public Vector3 HitPoint { get; private set; }
    public UnityEvent OnDie { get; set; }
    public UnityEvent OnGetHit { get; set; }

    private void Awake()
    {
        _agentStateCheck = GetComponent<AgentStateCheck>();
    }

    private void Start()
    {
        //Health = _agentStatus.maxHp;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_agentStateCheck.IsDead == true) return;
        if (_agentStateCheck.IsInvincibility == true) return;


        Health -= damage;
        OnGetHit?.Invoke();
        if (Health <= 0)
        {
            OnDie?.Invoke();
            _agentStateCheck.IsDead = true;
        }

    }

    public void CharacterChange()
    {

    }
}
