using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AgentSubWeapon : MonoBehaviour
{
    [SerializeField] private List<SubWeaponController> _subWeapons = new List<SubWeaponController>();

    private int _cnt = 0;
    private void Start()
    {
        PEventManager.StartListening("CardAdd", ActiveAttack);
    }

    private void ActiveAttack(Param param)
    {
        ESubWeaponType type = (ESubWeaponType)param.iParam;
        type = (ESubWeaponType)(++_cnt);
        var weapon = _subWeapons.Find(x => x.Type == type);
        Debug.Log(transform.root.name);
        weapon.ActiveWeapon();
    }
    private void OnDestroy()
    {
        PEventManager.StopListening("CardAdd", ActiveAttack);
    }
}
