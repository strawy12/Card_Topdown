using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class PlayerStatusManager : MonoSingleton<PlayerStatusManager>
{
    private AgentStatusSO _agentStatus;
    private PlayerStat _dynamicStatus;

    private AgentMove _agentMove = null;
    // 플레이어가 필요한가?
    private Player _player = null;

    #region 스테이터스 변수들
    [SerializeField] private AgentStatusSO _mountainStatus;
    [SerializeField] private AgentStatusSO _sunStatus;
    [SerializeField] private AgentStatusSO _riverStatus;
    [SerializeField] private AgentStatusSO _rockStatus;
    [SerializeField] private AgentStatusSO _cloudStatus;
    [SerializeField] private AgentStatusSO _bambooStatus;
    [SerializeField] private AgentStatusSO _pineStatus;
    [SerializeField] private AgentStatusSO _turtleStatus;
    [SerializeField] private AgentStatusSO _craneStatus;
    [SerializeField] private AgentStatusSO _deerStatus;
    #endregion

    public PlayerStat DynamicPlayerStatus { get => _dynamicStatus; set => _dynamicStatus = value; }

    private void Awake()
    {
        _dynamicStatus = new PlayerStat();
        StatusSet();
    }

    public void StatusSet()
    {
        // 이름가지고 스테이터스 가져와서 할거임
        // 근데 생각해보니까 안될거같아서 그냥 스위치로 일단함
        // 더 좋은생각나면 바꾸기 ㄱㄱ
        switch (PlayerRef.name)
        {
            case "Mountain":
                _agentStatus = _mountainStatus;
                break;
            case "Sun":
                _agentStatus = _sunStatus;
                break;
            case "River":
                _agentStatus = _riverStatus;
                break;
            case "Rock":
                _agentStatus = _rockStatus;
                break;
            case "Cloud":
                _agentStatus = _cloudStatus;
                break;
            case "Bamboo":
                _agentStatus = _bambooStatus;
                break;
            case "Pine":
                _agentStatus = _pineStatus;
                break;
            case "Turtle":
                _agentStatus = _turtleStatus;
                break;
            case "Crane":
                _agentStatus = _craneStatus;
                break;
            case "Deer":
                _agentStatus = _deerStatus;
                break;
        }

        if (_agentMove == null)
            _agentMove = PlayerRef.GetComponent<AgentMove>();
        if (_player == null)
            _player = PlayerRef.GetComponent<Player>();

        _dynamicStatus = new PlayerStat
        {
            maxHp = _agentStatus.maxHp,
            attackDamage = _agentStatus.attackDamage,
            attackSpeed = _agentStatus.attackSpeed,
            defence = _agentStatus.defence,
            criticalPercent = _agentStatus.criticalPercentage,
            criticalDamage = _agentStatus.criticalDamage,
            CooldownDecrease = _agentStatus.CooldownDecrease,
            maxSpeed = _agentMove.moveData.maxSpeed
        };
    }
}
