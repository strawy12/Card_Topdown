using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private AgentStatusSO _agentStatus;
    [SerializeField] private PlayerStat _dynamicStatus;
    public AgentStatusSO OriginalPlayerStatus { get => _agentStatus; }
    public PlayerStat DynamicPlayerStatus { get => _dynamicStatus; }

    public void Start()
    {
        StatusSet();
    }

    //public void AgentStatusSet()
    //{

    //}

    public void StatusSet()
    {
        _dynamicStatus = new PlayerStat();
        _dynamicStatus.maxHp = _agentStatus.maxHp;
        _dynamicStatus.attackDamage = _agentStatus.attackDamage;
        _dynamicStatus.attackSpeed = _agentStatus.attackSpeed;
        _dynamicStatus.defence = _agentStatus.defence;
        _dynamicStatus.criticalPercent = _agentStatus.criticalPercentage;
        _dynamicStatus.criticalDamage = _agentStatus.criticalDamage;
        _dynamicStatus.CooldownDecrease = _agentStatus.CooldownDecrease;
    }
}
