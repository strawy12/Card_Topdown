using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    // 각종 쿨타임이나 지속시간 등 쓰기
    public enum SkillState
    {
        READY,
        ING,
        END, // 필요할까..?
        PASSIVE
    }

    public float SkillCoolDown { get; set; }
    public float SkillDuration { get; set; }
    public bool IsPassive { get; set; }
}
