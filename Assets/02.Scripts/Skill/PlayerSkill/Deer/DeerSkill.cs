using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class DeerSkill : AgentSkill, INormalSkill
{
    [SerializeField] private float _skillCoolDown = 5f;
    public float SkillCoolDown => _skillCoolDown;

    [SerializeField] private float _skillCoolDownTimeCheck = 0f;
    public float SkillCoolDownTimeCheck { get => _skillCoolDownTimeCheck; set => _skillCoolDownTimeCheck = value; }

    [SerializeField] private float enemyStopTime = 3f;
    [SerializeField] private float distance = 20f;
    [SerializeField] private float time = 0.25f;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private float _force = 1f;
    [SerializeField] private LayerMask _enemyLayer;

    private bool _isSkillUsing = false;

    private Rigidbody2D _rb2D = null;

    private AgentStateCheck _agentStateCheck = null;

    private SkillDataSO _skillData = null;

    private void Awake()
    {
        _agentStateCheck = gameObject.GetComponent<AgentStateCheck>();
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
        _skillData = gameObject.GetComponent<Player>().SkillData;
        _skillCoolDown = _skillData.SkillCoolDown;
        SkillCoolDownTimeCheck = SkillCoolDown;
    }

    private void Update()
    {
        _skillCoolDownTimeCheck += Time.deltaTime;

        if (_isSkillUsing == false) return;
        Collider2D[] enemys = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);
        foreach (Collider2D enemy in enemys)
        {
            IKnockback enemyKnock = enemy.GetComponent<IKnockback>();

            Vector3 forceDir = enemy.transform.position - PlayerRef.transform.position;
            forceDir.Normalize();

            enemy.transform.Translate(forceDir * _force);

            AgentStateCheck enemyStateCheck = enemy.GetComponent<AgentStateCheck>();

            StartCoroutine(EnemyStopCoroutine(enemyStateCheck));

            //enemyKnock?.KnockBack(forceDir, force, 1f);
        }
    }

    public void SkillUsing()
    {
        if (_agentStateCheck.IsStop == true) return;
        if (_skillCoolDown > _skillCoolDownTimeCheck) return;
        _skillCoolDownTimeCheck = 0f;
        _agentStateCheck.IsStop = true;
        _isSkillUsing = true;

        Vector2 playerDir = MousePos - PlayerRef.transform.position;

        _rb2D.velocity = playerDir.normalized * distance;

        StartCoroutine(Skill());
    }

    IEnumerator Skill()
    {
        yield return new WaitForSeconds(time);

        _rb2D.velocity = Vector2.zero;
        _agentStateCheck.IsStop = false;
        _isSkillUsing = false;
    }

    IEnumerator EnemyStopCoroutine(AgentStateCheck enemyState)
    {
        enemyState.IsStop = true;
        yield return new WaitForSeconds(enemyStopTime);
        enemyState.IsStop = false;
    }

    public override void Reset()
    {
        StopAllCoroutines();
        _isSkillUsing = false;
        _agentStateCheck.IsStop = false;
    }
}
