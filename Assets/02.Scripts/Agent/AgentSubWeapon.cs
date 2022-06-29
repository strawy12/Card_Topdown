using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AgentSubWeapon : MonoBehaviour
{
    [SerializeField] private List<SubWeaponController> _subWeapons = new List<SubWeaponController>();

    private void Start()
    {
        PEventManager.StartListening("CardAdd", ActiveAttack);
    }

    private void ActiveAttack(Param param)
    {
        ESubWeaponType type = (ESubWeaponType)param.iParam;

        var weapon = _subWeapons.Find(x => x.Type == type);

        weapon.ActiveWeapon();
        AgentAudio
    }
}
