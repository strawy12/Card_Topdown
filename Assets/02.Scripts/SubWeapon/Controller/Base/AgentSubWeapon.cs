using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSubWeapon : MonoBehaviour
{
    [SerializeField] protected SubWeaponSO _weaponData;
    [SerializeField] private SubWeapon _subWeaponPrefab;
    [SerializeField] private int _initCreateCnt;

    protected List<SubWeapon> poolWeaponList = new List<SubWeapon>();

    private void Awake()
    {
        
    }

    protected virtual void ChildAwake()
    {

    }

    private void Start()
    {
        PoolManager.inst.CreatePool(_subWeaponPrefab, _initCreateCnt);
    }

    protected virtual void ChildStart()
    {

    }

    [ContextMenu("11")]
    public void ActiveAttack()
    {
        StartCoroutine(CoAttackLoop());
    }

    protected virtual void ChildActiveAttack()
    {

    }

    private IEnumerator CoAttackLoop()
    {
        while(true)
        {
            ChildAttackLoop();
            yield return new WaitForSeconds(_weaponData.delayTime);
        }
    }

    protected virtual void ChildAttackLoop()
    {

    }

    public SubWeapon GetWeaponObject()
    {
        SubWeapon weapon = PoolManager.inst.Pop(_subWeaponPrefab.name) as SubWeapon;
        weapon.InitWeapon(_weaponData.damage, _weaponData.lifeTime);

        return weapon;
    }
}
