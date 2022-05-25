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

    // 스킬 켜고 끄는거에 쿨타임 있으면 사용
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
