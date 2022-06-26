using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class MountainSkill : AgentSkill, NormalSkill
{
    [SerializeField] private float _skillCoolDown = 5f;
    public float SkillCoolDown => _skillCoolDown;

    public float SkillCoolDownTimeCheck { get; set; }

    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float force = 5f; 

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
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, force, _enemyLayer);
        foreach (Collider2D enemy in enemys)
        {
            IKnockback enemyKnock = enemy.GetComponent<IKnockback>();

            Vector3 forceDir = enemy.transform.position - PlayerTrm.position;
            forceDir.Normalize();

            enemy.transform.Translate(forceDir * force);

            //enemyKnock?.KnockBack(forceDir, force, 1f);
        }

        // overlap으로 주변 적을 찾음
        // 있으면 foreach로 플레이어와 적 위치 계산해서
        // 해당 방향으로 addforce?
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
            Gizmos.DrawWireSphere(transform.position, force);
        }
    }
#endif
}
