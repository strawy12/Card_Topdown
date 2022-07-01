using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSkill : AgentSkill, INormalSkill
{
    [SerializeField] private float _skillCoolDown = 5f;
    public float SkillCoolDown => _skillCoolDown;

    [SerializeField] private float _skillCoolDownTimeCheck = 0f;
    public float SkillCoolDownTimeCheck { get => _skillCoolDownTimeCheck; set => _skillCoolDownTimeCheck = value; }

    private void Awake()
    {
        _skillCoolDownTimeCheck = _skillCoolDown;
    }

    private void Update()
    {
        _skillCoolDownTimeCheck += Time.deltaTime;
    }

    public void SkillUsing()
    {
        if (_skillCoolDown > _skillCoolDownTimeCheck) return;
        _skillCoolDownTimeCheck = 0f;

        CloudFog trap = PoolManager.Inst.Pop("CloudFog") as CloudFog;
        trap.transform.position = transform.position;
    }
}
