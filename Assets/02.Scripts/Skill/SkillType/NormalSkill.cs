using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INormalSkill
{
    public float SkillCoolDown { get; }
    public float SkillCoolDownTimeCheck { get; set; }

    public void SkillUsing();
}
