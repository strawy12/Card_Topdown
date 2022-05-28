using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NormalSkill : AgentSkill
{

    
    protected enum BuffSkillState
    {
        READY,
        ING,
        WAIT,
        CHARGING
    }

    protected virtual float SkillCoolDown { get; set; }

    protected virtual void Awake()
    {
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }

    protected virtual void SkillUsing()
    {

    }
}
