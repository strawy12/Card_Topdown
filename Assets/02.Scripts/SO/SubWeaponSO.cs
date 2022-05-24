using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Weapon/SubWeaponSO")]
public class SubWeaponSO : ScriptableObject
{
    public float delayTime;
    public float damage;

    public bool isEnemy;
    public bool attackWeapon;

}
