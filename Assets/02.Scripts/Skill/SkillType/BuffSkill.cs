using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuffSkill : MonoBehaviour
{
    protected enum BuffSkillState
    {
        READY,
        ING,
        WAIT
    }

    protected virtual float SkillCoolDown { get; set; }
    protected virtual float SkillDuration { get; set; }

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
