using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentSkill : MonoBehaviour
{
    protected enum SkillState
    {
        READY,
        ING,
        END, // 필요할까..?
        PASSIVE,
        SkillOn,
        SkillOff
    }

    protected virtual float SkillCoolDown { get; set; }
    protected virtual float SkillDuration { get; set; }
    protected virtual bool IsPassive { get; set; }

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }

    protected virtual IEnumerator SkillUsing(float skillDuration)
    {
        yield return new WaitForSeconds(skillDuration);
    }
}
