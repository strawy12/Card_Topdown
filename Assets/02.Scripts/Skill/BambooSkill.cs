using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class BambooSkill : AgentSkill, INormalSkill
{
    [SerializeField] private float _skillCoolDown = 0;
    public float SkillCoolDown => _skillCoolDown;

    [SerializeField] private float _skillCoolDownTimeCheck = 0;
    public float SkillCoolDownTimeCheck 
    {
        get => _skillCoolDownTimeCheck;
        set => _skillCoolDownTimeCheck = value;
    }

    private void Awake()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    private void Update()
    {
        SkillCoolDownTimeCheck += Time.deltaTime;
    }

    public void SkillUsing()
    {
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillCoolDownTimeCheck = 0f;
        PoolableMono bambooSpear = PoolManager.Inst.Pop("BambooSpear");
        Vector3 dir = MousePos - PlayerTrm.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bambooSpear.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        bambooSpear.transform.position = PlayerTrm.position;
    }

    protected override void Reset()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
        StopAllCoroutines();
    }
}
