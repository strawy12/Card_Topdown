using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownSkill : MonoBehaviour//, ISkill
{
    //AgentStatusSO _playerStatus;
    //AgentStatusSO _dynamicStatus;

    //[field: SerializeField] public float SkillCoolDown { get; set; }
    //[field: SerializeField] public float SkillDuration { get; set; }
    //[field: SerializeField] public bool IsPassive { get; set; }

    //[SerializeField] private float attackIncrement = 0;

    public enum SkillState
    {
        READY,
        ING,
        END, // 필요할까..?
        PASSIVE
    }

    //private void Start()
    //{
    //    _playerStatus = GetComponent<Player>().PlayerStatus;
    //    _dynamicStatus = GetComponent<Player>().DynamicPlayerStatus;
    //}



    //IEnumerator SkillUsing()
    //{
    //    yield return new WaitForSeconds(SkillDuration);
    //    _dynamicStatus.attackDamage = _playerStatus.attackDamage;
    //    Debug.Log(_dynamicStatus.attackDamage);
    //}
}
