using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineSkill : AgentSkill, INormalSkill
{
    [SerializeField] private float _skillCoolDown = 3f;
    public float SkillCoolDown => _skillCoolDown;

    [SerializeField] private float _skillCoolDownTimeCheck = 0f;
    public float SkillCoolDownTimeCheck { get => _skillCoolDownTimeCheck; set => _skillCoolDownTimeCheck = value; }

    [SerializeField] private float _sizeX = 3f;
    [SerializeField] private float _sizeY = 3f;

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

    public void SkillUsing()
    {
        if (_skillCoolDown > _skillCoolDownTimeCheck) return;
        _skillCoolDownTimeCheck = 0f;

        PineTrap trap = PoolManager.Inst.Pop("PineTrap") as PineTrap;
        float trapTrmX = transform.position.x + Random.Range(-_sizeX, _sizeX);
        float trapTrmY = transform.position.y + Random.Range(-_sizeY, _sizeY);
        trap.transform.position = new Vector2(trapTrmX, trapTrmY);
    }

}
