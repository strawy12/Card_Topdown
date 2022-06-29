using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : AgentSkill
{
    public float SkillCoolDown;
    public float SkillDuration;
    public float SkillCoolDownTimeCheck;

    protected virtual void Awake()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    protected virtual void Update()
    {
        SkillCoolDownTimeCheck += Time.deltaTime;
    }

    public virtual void Skill()
    {

    }

    protected virtual IEnumerator SkillUsing(float skillDuration)
    {
        // ���� ����
        yield return new WaitForSeconds(skillDuration);
        // ���� ���� ��
    }
}
