using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnOffSkill : AgentSkill
{
    protected enum OnOffSkillState
    {
        READY,
        ING,
        WAIT
    }

    // ��ų �Ѱ� ���°ſ� ��Ÿ�� ������ ���
    protected virtual float SkillCoolDown { get; set; }
    protected virtual float SkillCoolDownTimeCheck { get; set; }
    protected virtual bool IsSkillOn { get; set; }

    protected virtual void SkillUsing()
    {
        if (IsSkillOn == false)
            SkillOn();
        else if (IsSkillOn == true)
            SkillOff();
    }

    protected virtual void SkillOn()
    {

    }
    protected virtual void SkillOff()
    {

    }
}
