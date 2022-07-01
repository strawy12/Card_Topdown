using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneSkill : BuffSkill
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private float enemyStopTime = 3f;

    private BoxCollider2D boxCol2D = null;

    List<GameObject> enemy = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        boxCol2D = gameObject.GetComponent<BoxCollider2D>();
    }

    protected override IEnumerator SkillUsing(float skillDuration)
    {
        boxCol2D.enabled = false;
        isBuffOn = true;
        yield return new WaitForSeconds(skillDuration);
        boxCol2D.enabled = true;
        isBuffOn = false;
    }           

    public void Skill()
    {
        if (SkillCoolDown > SkillCoolDownTimeCheck) return;
        SkillCoolDownTimeCheck = 0f;
        StartCoroutine(SkillUsing(SkillDuration));
    }

    IEnumerator EnemyStunCoroutine(AgentStateCheck enemyState)
    {
        enemyState.IsStop = true;
        yield return new WaitForSeconds(enemyStopTime);
        enemyState.IsStop = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && isBuffOn == true)
        {
            AgentStateCheck enemyState = collision.GetComponent<AgentStateCheck>();    
            StartCoroutine(EnemyStunCoroutine(enemyState));
        }
    }

    protected override void Reset()
    {
        base.Reset();
        boxCol2D.enabled = true;
    }
}
