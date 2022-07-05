using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="SO/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public float SkillCoolDown = 5f;
    public float Damage = 5f;
}
