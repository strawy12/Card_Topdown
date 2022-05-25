using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunbiSkill : AgentSkill
{
    [SerializeField] private AgentStatusSO _originalPlayerStatus;
    [SerializeField] private AgentStatusSO _dynamicPlayerStatus;

    [SerializeField] private float attackIncrement = 0;
    // ��ų ��ٿ� üũ�Ұ���
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
        // ��ų ��Ÿ�� ���Ҵ��� üũ
        if (SunbiSkillCoolDown >= skillCoolDownTimeCheck) return;
        if (skillState == SkillState.READY) // ��ų �Ⱦ��� ����
        {
            skillState = SkillState.ING;
            skillCoolDownTimeCheck = 0;
            _dynamicPlayerStatus.attackDamage += attackIncrement;
        }
        else if (skillState == SkillState.ING) // ��ų ��������
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
