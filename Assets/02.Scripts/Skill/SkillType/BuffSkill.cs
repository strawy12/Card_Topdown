using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSkill : AgentSkill
{
    public float SkillCoolDown;
    public float SkillDuration;
    public float SkillCoolDownTimeCheck;
    public bool isBuffOn = false;

    protected virtual void Awake()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    protected virtual void Update()
    {
        SkillCoolDownTimeCheck += Time.deltaTime;
    }

    protected virtual IEnumerator SkillUsing(float skillDuration)
    {
        // 버프 내용
        yield return new WaitForSeconds(skillDuration);
        // 버프 끝날 때
    }
}
