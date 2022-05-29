using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;

public class Player : MonoBehaviour, IAgent, IHittable
{
    private AgentStateCheck _agentStateCheck;

    [SerializeField]
    private float _initHp;

    private float _health;

    public float Health
    {
        get => _health;
        set
        {
            //_health = Mathf.Clamp(value, 0, _agentStatus.maxHp);
            _health = Mathf.Clamp(value, 0, _initHp);
        }
    }


    public bool IsEnemy => false;
    public Vector3 HitPoint { get; private set; }
    [field:SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [SerializeField]
    private HpBar _playerHpBar = null;

    private void Awake()
    {
        _agentStateCheck = GetComponent<AgentStateCheck>();

        if(_playerHpBar == null)
        {
            _playerHpBar = transform.Find("HpBar").GetComponent<HpBar>();
        }
    }

    private void Start()
    {
        _health = _initHp;
        //Health = _agentStatus.maxHp;
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (_agentStateCheck.IsDead == true) return;
        if (_agentStateCheck.IsInvincibility == true) return;


        Health -= damage;
        OnGetHit?.Invoke();

        _playerHpBar.HpBarGaugeSetting(_health / _initHp);

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
