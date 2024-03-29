using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSkill : AgentSkill, INormalSkill
{
    [SerializeField] private float _skillCoolDown = 1f;
    public float SkillCoolDown => _skillCoolDown;

    public float SkillCoolDownTimeCheck { get; set; }

    [SerializeField] private float stunTime = 3f;

    [SerializeField] private float circleRange = 5f;

    [SerializeField] private LayerMask _enemyLayer;

    private SkillDataSO _skillData = null;

    private void Awake()
    {
        _skillData = gameObject.GetComponent<Player>().SkillData;
        _skillCoolDown = _skillData.SkillCoolDown;
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    private void Update()
    {
        SkillCoolDownTimeCheck += Time.deltaTime;
    }

    // 나중에는 얘가 적관리하면서 스택 관리하자
    public void SkillUsing()
    {
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillCoolDownTimeCheck = 0;

        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, circleRange, _enemyLayer);
        foreach (Collider2D enemy in enemys)
        {
            Enemy enemySc = enemy.GetComponent<Enemy>();
            enemySc._waterStack += 1;
            enemySc.CheckWaterStack(stunTime);
        }
    }

    public override void Reset()
    {
        SkillCoolDownTimeCheck = SkillCoolDown;
        StopAllCoroutines();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, circleRange);
        }
    }
#endif
}
