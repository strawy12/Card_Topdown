using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSkill : AgentSkill
{
    protected enum BuffSkillState
    {
        READY,
        ING,
        WAIT
    }

    [field: SerializeField] protected virtual float SkillCoolDown { get; set; }
    [field: SerializeField] protected virtual float SkillDuration { get; set; }
    [field: SerializeField] protected virtual float skillCoolDownTimeCheck { get; set; }

    protected virtual void Awake()
    {
        ChildAwake();
    }



    protected virtual void ChildAwake()
    {

    }

    protected virtual IEnumerator SkillUsing(float skillDuration)
    {
        yield return new WaitForSeconds(skillDuration);
    }
}
