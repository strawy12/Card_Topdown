using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Agent/Status")]
public class AgentStatusSO : ScriptableObject
{
    [Header("PlayerStatus")]
    [Space(10f)]
    public float maxHp;
    [Space(10f)]
    public float attackDamage;
    public float attackSpeed;
    // public float moveSpeed; MovedataSO에 들어있음
    public float defence;
    [Space(10f)]
    public float criticalPercentage;
    public float criticalDamage;
    [Space(10f)]
    public float CooldownDecrease;
}
