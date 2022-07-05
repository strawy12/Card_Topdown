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
//        if (SkillCoolDown >= skillCoolDownTimeCheck) return; // 스킬 쿨타임 돌았는지 체크
//        skillCoolDownTimeCheck = 0;                               // 쿨타임 다시 돌림
//        _dynamicPlayerStatus.attackDamage += attackIncrement;     // 공격력 증가
//        StartCoroutine(SkillUsing(SkillDuration));           // 코루틴 실행
//    }

//    protected override IEnumerator SkillUsing(float skillDuration)
//    {
//        yield return new WaitForSeconds(skillDuration);           // 지속시간 끝난 뒤 
//        ResetStatus();
//        _dynamicPlayerStatus.attackDamage = _originalPlayerStatus.attackDamage; // 원래 공격력으로 돌리기
//    }

//    private void ResetSkill()
//    {
//        skillCoolDownTimeCheck = SkillCoolDown;
//    }

//    private void ResetStatus()
//    {
//        _dynamicPlayerStatus.attackDamage = _originalPlayerStatus.attackDamage; // 원래 공격력으로 돌리기
//    }

//    protected override void Reset() // 캐릭터 바꿀 때 할듯?
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
