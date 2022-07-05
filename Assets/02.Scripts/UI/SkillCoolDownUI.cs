using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UtilDefine;

public class SkillCoolDownUI : MonoBehaviour
{
    [SerializeField]
    private Image fillArea = null;
    [SerializeField]
    private SkillDataSO _skillData = null;

    private float time = 0;

    private void Awake()
    {
        _skillData = PlayerRef.GetComponent<Player>().SkillData;
        fillArea.fillAmount = 0f;
    }

    private void Update()
    {
        if (time > 0)
            time -= Time.deltaTime;
        fillArea.fillAmount = time / _skillData.SkillCoolDown;
    }

    public void StartTimer()
    {
        if (0 < time) return;
        fillArea.fillAmount = 1f;
        time = _skillData.SkillCoolDown;
    }

    public void ChangeCharacter()
    {
        time = 0;
        fillArea.fillAmount = 0f;
    }
}
