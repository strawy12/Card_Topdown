using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    // ���� ��Ÿ���̳� ���ӽð� �� ����
    public enum SkillState
    {
        READY,
        ING,
        END, // �ʿ��ұ�..?
        PASSIVE
    }

    public float SkillCoolDown { get; set; }
    public float SkillDuration { get; set; }
    public bool IsPassive { get; set; }
}
