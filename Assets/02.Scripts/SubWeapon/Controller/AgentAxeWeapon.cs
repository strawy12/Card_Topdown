using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAxeWeapon : AgentSubWeapon
{
    [SerializeField] private float _throwForce;
    [SerializeField] private float _spreadRange;
    [SerializeField] private float _dropForce;
    [SerializeField] private float _rotateSpeed;
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
        StartCoroutine(SpawnAxeDelay());
    }

    private IEnumerator SpawnAxeDelay()
    {
        Axe axe;
        for (int i = 0; i < spawnCnt; i++)
        {
            axe = GetWeaponObject() as Axe;
            axe.InitAxe(_throwForce, _spreadRange, _dropForce, _rotateSpeed);
            axe.gameObject.SetActive(true);
            axe.transform.position = transform.position + _offset;
            axe.StartAttack();
            yield return new WaitForSeconds(0.5f); 
        }

    }
}
