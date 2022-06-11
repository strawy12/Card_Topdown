using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NormalSkill
{
    public float SkillCoolDown { get; }
    public float SkillCoolDownTimeCheck { get; set; }

    public void SkillUsing();
}
