using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSkill : AgentSkill, NormalSkill
{
    [SerializeField] private float _skillCoolDown = 1f;
    public float SkillCoolDown => _skillCoolDown;

    public float SkillCoolDownTimeCheck { get; set; }

    [SerializeField] private float stunTime = 3f;

    [SerializeField] private float circleRange = 5f;

    [SerializeField] private LayerMask _enemyLayer;

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
        SkillCoolDownTimeCheck = 0;
        Debug.Log("스킬사용");

        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, circleRange, _enemyLayer);
        foreach (Collider2D enemy in enemys)
        {
            Enemy enemySc = enemy.GetComponent<Enemy>();
            enemySc._waterStack += 1;
            enemySc.CheckWaterStack(stunTime);
        }
    }

    protected override void Reset()
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
