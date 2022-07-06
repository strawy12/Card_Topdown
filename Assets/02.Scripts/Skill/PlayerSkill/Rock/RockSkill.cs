using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class RockSkill : BuffSkill, IHittable
{
    private Player player = null;

    private SkillDataSO _skillData = null;

    protected override void Awake()
    {
        _skillData = gameObject.GetComponent<Player>().SkillData;
        SkillCoolDown = _skillData.SkillCoolDown;
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    private bool isOn = false;

    public bool IsEnemy => false;

    public Vector3 HitPoint { get; set; }
    
    public void Skill()
    {
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillCoolDownTimeCheck = 0;
        StartCoroutine(SkillUsing(SkillDuration));
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (isOn == false) return;
        
        IKnockback enemy = damageDealer.GetComponent<IKnockback>();

        Vector3 dir = damageDealer.transform.position - PlayerRef.transform.position;
        dir.Normalize();

        damageDealer.transform.Translate(dir * 10);

        //enemy.KnockBack(dir, 1f, 1f);
    }

    protected override IEnumerator SkillUsing(float skillDuration)
    {
        PlayerStatusManager.Inst.DynamicPlayerStatus.defence += 5;
        isOn = true;
        yield return new WaitForSeconds(skillDuration);
        PlayerStatusManager.Inst.DynamicPlayerStatus.defence -= 5;
        isOn = false;    
    }

    public override void Reset()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
        StopAllCoroutines();
    }

    public void GetCrowdCtrl(int types, float amount)
    {

    }
}
