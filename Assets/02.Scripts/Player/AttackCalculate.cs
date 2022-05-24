using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class AttackCalculate : MonoBehaviour
{
    public float beforeDelay = 0.25f;

    public void StartDelay()
    {
        StartCoroutine(BeforeDelay());
    }


    IEnumerator BeforeDelay()
    {
        yield return new WaitForSeconds(beforeDelay);
        
        StartSpawnEffect();
    }

    private void StartSpawnEffect()
    {
        AttackStart attackEffect = PoolManager.inst.Pop("DefaultAttackEffect") as AttackStart;
        Vector2 direction = MousePos - attackEffect.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackEffect.transform.position = new Vector2(PlayerTrm.position.x, PlayerTrm.position.y + 0.75f);
        attackEffect.transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        attackEffect.transform.position = attackEffect.transform.position - attackEffect.transform.up;
        CalculateAttack();
    }

    public void CalculateAttack() // 공격 계산 오버랩박스로
    {
        // 공격을 계산할거임
    }

}
