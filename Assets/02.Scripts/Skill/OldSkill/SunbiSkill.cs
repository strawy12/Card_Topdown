//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SunbiSkill : BuffSkill
//{
//    [SerializeField] private float attackIncrement = 0;

//    [SerializeField] private AgentStatusSO _originalPlayerStatus;
//    [SerializeField] private PlayerStat _dynamicPlayerStatus;

//    public float SkillCoolDown { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    public float SkillDuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
//    public float skillCoolDownTimeCheck { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

//    protected override void Awake()
//    {
//        _originalPlayerStatus = GetComponent<PlayerStatus>().OriginalPlayerStatus;
//        _dynamicPlayerStatus = GetComponent<PlayerStatus>().DynamicPlayerStatus;
//        ResetSkill();
//    }

//    protected void Update()
//    {
//        skillCoolDownTimeCheck += Time.deltaTime;
//    }

//    public void KnowLedgeIsPower()
//    {
//        if (SkillCoolDown >= skillCoolDownTimeCheck) return; // ��ų ��Ÿ�� ���Ҵ��� üũ
//        skillCoolDownTimeCheck = 0;                               // ��Ÿ�� �ٽ� ����
//        _dynamicPlayerStatus.attackDamage += attackIncrement;     // ���ݷ� ����
//        StartCoroutine(SkillUsing(SkillDuration));           // �ڷ�ƾ ����
//    }

//    protected override IEnumerator SkillUsing(float skillDuration)
//    {
//        yield return new WaitForSeconds(skillDuration);           // ���ӽð� ���� �� 
//        ResetStatus();
//        _dynamicPlayerStatus.attackDamage = _originalPlayerStatus.attackDamage; // ���� ���ݷ����� ������
//    }

//    private void ResetSkill()
//    {
//        skillCoolDownTimeCheck = SkillCoolDown;
//    }

//    private void ResetStatus()
//    {
//        _dynamicPlayerStatus.attackDamage = _originalPlayerStatus.attackDamage; // ���� ���ݷ����� ������
//    }

//    protected override void Reset() // ĳ���� �ٲ� �� �ҵ�?
//    {
//        ResetStatus();
//        ResetSkill();
//        StopAllCoroutines();
//    }

//    IEnumerator BuffSkill.SkillUsing(float skillDuration)
//    {
//        throw new NotImplementedException();
//    }
//}
