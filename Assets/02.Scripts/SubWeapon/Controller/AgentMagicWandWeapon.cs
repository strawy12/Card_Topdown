using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMagicWandWeapon : AgentSubWeapon
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private int spawnCnt;
    [SerializeField] private Vector3 _offset;

    [Header("·£´ý¿ë")]
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
        MagicWand magicWand;
        for (int i = 0; i < spawnCnt; i++)
        {
            magicWand = GetWeaponObject() as MagicWand;
            magicWand.InitMagicWand(moveSpeed);
            magicWand.gameObject.SetActive(true);
            magicWand.transform.position = transform.position + _offset;
            magicWand.StartAttack();
            yield return new WaitForSeconds(0.5f);
        }

    }
}
