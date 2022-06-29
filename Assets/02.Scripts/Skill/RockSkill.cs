using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class RockSkill : BuffSkill, IHittable
{
    private Player player = null;

    protected override void Awake()
    {
        base.Awake();
        player = PlayerTrm.GetComponent<Player>();
    }

    private bool isOn = false;

    public bool IsEnemy => false;

    public Vector3 HitPoint { get; set; }
    
    public override void Skill()
    {
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillCoolDownTimeCheck = 0;
        Debug.Log("스킬 누름");
        StartCoroutine(SkillUsing(SkillDuration));
    }

    public void GetHit(float damage, GameObject damageDealer)
    {
        if (isOn == false) return;
        
        IKnockback enemy = damageDealer.GetComponent<IKnockback>();

        Vector3 dir = damageDealer.transform.position - PlayerTrm.position;
        dir.Normalize();

        damageDealer.transform.Translate(dir * 10);
        Debug.Log("밀었음");

        //enemy.KnockBack(dir, 1f, 1f);
    }

    protected override IEnumerator SkillUsing(float skillDuration)
    {
        Debug.Log("스킬 실행");
        PlayerStatusManager.Inst.DynamicPlayerStatus.defence += 5;
        isOn = true;
        yield return base.SkillUsing(skillDuration);
        Debug.Log("스킬 끝남");
        PlayerStatusManager.Inst.DynamicPlayerStatus.defence -= 5;
        isOn = false;    
    }

    protected override void Reset()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
        StopAllCoroutines();
    }
}
