using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWhipWeapon : SubWeaponController
{
    [SerializeField] private int spawnCnt;
    [SerializeField] private Vector3 _offset;

    // 채찍이 플레이어 앞으로 쏴야하는지 뒤로 쏴야하는지 알려주는 Boolean변수
    private bool _isTurn = false;

    [Header("랜덤용")]
    [SerializeField] private bool _isRandomCnt;

    protected override void ChildAttackLoop()
    {
        if (_isRandomCnt)
        {
            spawnCnt = Random.Range(1, 12);
        }
        StartCoroutine(SpawnWandDelay());
    }

    private IEnumerator SpawnWandDelay()
    {
        Whip whip;
        for (int i = 0; i < spawnCnt; i++)
        {
            whip = GetWeaponObject() as Whip;
            whip.InitWhip(_isTurn);
            whip.gameObject.SetActive(true);
            whip.transform.position = transform.position + _offset;
            whip.StartAttack();
            yield return new WaitForSeconds(0.5f);
        }

    }
}
