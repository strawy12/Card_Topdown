using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnOffSkill : MonoBehaviour
{
    protected enum OnOffSkillState
    {
        READY,
        ING,
        WAIT
    }

    // ��ų �Ѱ� ���°ſ� ��Ÿ�� ������ ���
    protected virtual float SkillCoolDown { get; set; }

    protected virtual void Awake()
    {
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }

    protected virtual void SkillUsing(OnOffSkillState skillState)
    {
        if (skillState == OnOffSkillState.WAIT) return;
        if (skillState == OnOffSkillState.READY)
            SkillOn();
        else if (skillState == OnOffSkillState.ING)
            SkillOff();
    }

    protected virtual void SkillOn()
    {

    }
    protected virtual void SkillOff()
    {

    }
}
