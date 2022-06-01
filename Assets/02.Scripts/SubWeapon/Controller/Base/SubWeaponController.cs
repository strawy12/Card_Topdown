using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESubWeaponType = AgentSubWeapon.ESubWeaponType;

public class SubWeaponController : MonoBehaviour
{
    [SerializeField] protected ESubWeaponType _subWeaponType;
    [SerializeField] protected SubWeaponSO _weaponData;
    [SerializeField] private SubWeapon _subWeaponPrefab;
    [SerializeField] private int _initCreateCnt;

    protected List<SubWeapon> poolWeaponList = new List<SubWeapon>();

    public ESubWeaponType Type { get => _subWeaponType; }
    public bool IsActive { get => _isActive; set => _isActive = value; }

    private bool _isActive = false;

    private void Awake()
    {
        
    }

    protected virtual void ChildAwake()
    {

    }

    private void Start()
    {
        PoolManager.Inst.CreatePool(_subWeaponPrefab, _initCreateCnt);
    }

    protected virtual void ChildStart()
    {

    }

    [ContextMenu("11")]
    public void ActiveAttack()
    {
        if (_isActive == false) return;

        StartCoroutine(CoAttackLoop());
    }

    protected virtual void ChildActiveAttack()
    {

    }

    private IEnumerator CoAttackLoop()
    {
        while(_isActive)
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

        SubWeapon weapon = PoolManager.Inst.Pop(_subWeaponPrefab.name) as SubWeapon;
        weapon.InitWeapon(_weaponData.damage, _weaponData.lifeTime);

        
        return weapon;
    }
}
