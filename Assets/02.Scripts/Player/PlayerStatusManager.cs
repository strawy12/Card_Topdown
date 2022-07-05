using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class PlayerStatusManager : MonoSingleton<PlayerStatusManager>
{
    private AgentStatusSO _agentStatus;
    private PlayerStat _dynamicStatus;

    private AgentMove _agentMove = null;
    // �÷��̾ �ʿ��Ѱ�?
    private Player _player = null;

    #region �������ͽ� ������
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
        // �̸������� �������ͽ� �����ͼ� �Ұ���
        // �ٵ� �����غ��ϱ� �ȵɰŰ��Ƽ� �׳� ����ġ�� �ϴ���
        // �� ������������ �ٲٱ� ����
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
