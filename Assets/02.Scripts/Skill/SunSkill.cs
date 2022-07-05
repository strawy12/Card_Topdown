using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class SunSkill : AgentSkill, NormalSkill
{
    [SerializeField] private float _skillCoolDown = 5f;
    public float SkillCoolDown => _skillCoolDown;

    public float SkillDuration = 1f;

    public float SkillCoolDownTimeCheck { get; set; }

    private void Awake()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    private void Update()
    {
        SkillCoolDownTimeCheck += Time.deltaTime;
    }

    public void SkillUsing()
    {
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillCoolDownTimeCheck = 0f;
        PoolableMono raser =  PoolManager.Inst.Pop("RaserParent");
        Vector3 dir = MousePos - PlayerRef.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        raser.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
        raser.transform.position = PlayerRef.transform.position;
        StartCoroutine(ShootRazer(raser));
    }

    IEnumerator ShootRazer(PoolableMono raser)
    {
        while(true)
        {
            float length = Mathf.Lerp(raser.transform.localScale.y, 10f, 0.4f);

            raser.transform.localScale = new Vector3(raser.transform.localScale.x,
                length, raser.transform.localScale.z);

            if (length > 9.9f)
                break;
            yield return new WaitForSeconds(0.1f);
        }
        PoolManager.Inst.Push(raser);
    }

    protected override void Reset()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
        StopAllCoroutines();
    }
}
