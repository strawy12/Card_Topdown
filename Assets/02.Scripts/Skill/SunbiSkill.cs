using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunbiSkill : AgentSkill
{
    [SerializeField] private AgentStatusSO _originalPlayerStatus;
    [SerializeField] private AgentStatusSO _dynamicPlayerStatus;

    [SerializeField] private float attackIncrement = 0;
    // 스킬 쿨다운 체크할거임
    private float skillCoolDownTimeCheck = 0;

    protected SkillState skillState;

    [field: SerializeField] protected float SunbiSkillCoolDown { get; set; }
    [field: SerializeField] protected float SunbiSkillDuration { get; set; }
    [field: SerializeField] protected bool SunbiIsPassive { get; set; }

    protected override void Start()
    {
        base.Start();
        _originalPlayerStatus = GetComponent<PlayerStatus>().OriginalPlayerStatus;
        _dynamicPlayerStatus = GetComponent<PlayerStatus>().DynamicPlayerStatus;
        InitSkill();
    }

    private void InitSkill()
    {
        skillState = SkillState.READY;
        skillCoolDownTimeCheck = SunbiSkillCoolDown;
    }

    protected void Update()
    {
        skillCoolDownTimeCheck += Time.deltaTime;
    }

    public void KnowLedgeIsPower()
    {
        // 스킬 쿨타임 돌았는지 체크
        if (SunbiSkillCoolDown >= skillCoolDownTimeCheck) return;
        if (skillState == SkillState.READY) // 스킬 안쓰는 상태
        {
            skillState = SkillState.ING;
            skillCoolDownTimeCheck = 0;
            _dynamicPlayerStatus.attackDamage += attackIncrement;
        }
        else if (skillState == SkillState.ING) // 스킬 쓰고있음
        {
            return;
        }
        StartCoroutine(SkillUsing(SunbiSkillDuration));
    }

    protected override IEnumerator SkillUsing(float skillDuration)
    {
        yield return StartCoroutine(base.SkillUsing(skillDuration));
        skillState = SkillState.READY;
        _dynamicPlayerStatus.attackDamage = _originalPlayerStatus.attackDamage;
    }

}
