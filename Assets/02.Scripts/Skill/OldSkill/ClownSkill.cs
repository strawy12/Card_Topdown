using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownSkill : OnOffSkill
{
    [SerializeField] private AgentStatusSO _originalPlayerStatus;
    [SerializeField] private PlayerStat _dynamicPlayerStatus;
    [SerializeField] private MoveDataSO _moveData;

    private float _attackDamageChangeValue;
    private float _attackSpeedChangeValue;
    private float _defenceChagneValue;
    private float _criticalPercentageChangeValue;
    private float _criticalDamageChangeValue;
    private float _speedChangeValue;

    protected virtual void Awake()
    {
        _dynamicPlayerStatus = GetComponent<PlayerStatusManager>().DynamicPlayerStatus;
        ResetSkill();
    }
    protected void Update()
    {
        SkillCoolDownTimeCheck += Time.deltaTime;
    }

    private void ResetSkill()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
        IsSkillOn = false;
    }

    private void ResetStatus()
    {

    }

    public void WorkStart()
    {
        // 스킬쿨타임이 다 안돌았으면 리턴
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillUsing(); // 스킬 실행
    }

    // 부모에 있음
    //protected override void SkillUsing()
    //{
    //    // 켜져있음
    //    if (IsSkillOn == true)
    //    { // 끄기
    //        SkillOff();
    //    }
    //    // 꺼져있음
    //    else if (IsSkillOn == false)
    //    { // 켜기
    //        SkillOn();
    //    }
    //}

    private void ChangeValueSet()
    {
        _speedChangeValue = _moveData.maxSpeed / 10;
        _attackDamageChangeValue = -5;
        _attackSpeedChangeValue = _originalPlayerStatus.attackSpeed / 10;
        _defenceChagneValue = -5;
        _criticalPercentageChangeValue = _originalPlayerStatus.criticalPercentage / 10;
        _criticalDamageChangeValue = _originalPlayerStatus.criticalDamage / 10;
    }

    protected override void SkillOn()
    {
        IsSkillOn = true;

        ChangeValueSet();

        _moveData.maxSpeed += _speedChangeValue;
        _dynamicPlayerStatus.attackDamage += _attackDamageChangeValue;
        _dynamicPlayerStatus.attackSpeed += _attackSpeedChangeValue;
        _dynamicPlayerStatus.defence += _defenceChagneValue;
        _dynamicPlayerStatus.criticalDamage += _criticalPercentageChangeValue;
        _dynamicPlayerStatus.criticalDamage += _criticalDamageChangeValue;
    }

    protected override void SkillOff()
    {
        IsSkillOn = false;

        _moveData.maxSpeed -= _speedChangeValue;
        _dynamicPlayerStatus.attackDamage -= _attackDamageChangeValue;
        _dynamicPlayerStatus.attackSpeed -= _attackSpeedChangeValue;
        _dynamicPlayerStatus.criticalDamage -= _criticalPercentageChangeValue;
        _dynamicPlayerStatus.criticalDamage -= _criticalDamageChangeValue;
    }

    protected override void Reset() // 캐릭터 바꿀 때 할듯?
    {
        ResetStatus();
        ResetSkill();
        StopAllCoroutines();
    }
}
