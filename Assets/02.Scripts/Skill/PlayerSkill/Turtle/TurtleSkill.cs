using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSkill : AgentSkill, INormalSkill
{
    [SerializeField] private float _skillCoolDown = 5f;
    public float SkillCoolDown => _skillCoolDown;

    [SerializeField] private float _skillCoolDownTimeCheck = 0f;
    public float SkillCoolDownTimeCheck { get => _skillCoolDownTimeCheck; set => _skillCoolDownTimeCheck = value; }

    [SerializeField] private bool isOn = false;

    private SkillDataSO _skillData = null;

    private void Awake()
    {
        _skillData = gameObject.GetComponent<Player>().SkillData;
        _skillCoolDown = _skillData.SkillCoolDown;
        SkillCoolDownTimeCheck = SkillCoolDown;
    }
    private void Update()
    {
        _skillCoolDownTimeCheck += Time.deltaTime;
    }

    public void Hit()
    {
        if (isOn == false) return;
        if (_skillCoolDown > _skillCoolDownTimeCheck) return;
        _skillCoolDownTimeCheck = 0f;
        isOn = false;
        PlayerStatusManager.Inst.DynamicPlayerStatus.defence += 5;
        PlayerStatusManager.Inst.DynamicPlayerStatus.maxHp += 50;

        PlayerStatusManager.Inst.DynamicPlayerStatus.attackDamage -= 8;
    }

    public void SkillUsing()
    {
        if (isOn) return;
        isOn = true;
        
        PlayerStatusManager.Inst.DynamicPlayerStatus.defence -= 5;
        PlayerStatusManager.Inst.DynamicPlayerStatus.maxHp -= 50;

        PlayerStatusManager.Inst.DynamicPlayerStatus.attackDamage += 8;

    }

    public override void Reset()
    {
        isOn = false;
    }
}
