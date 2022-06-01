using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWhipWeapon : SubWeaponController
{
    [SerializeField] private int spawnCnt;
    [SerializeField] private Vector3 _offset;

    // ä���� �÷��̾� ������ �����ϴ��� �ڷ� �����ϴ��� �˷��ִ� Boolean����
    private bool _isTurn = false;

    [Header("������")]
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
