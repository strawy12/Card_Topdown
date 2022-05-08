using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected Weapon _weapon;

    protected WeaponRenderer _weaponRenderer;

    protected float _disireAngle;

    private void Start()
    {
        _weapon = GetComponentInChildren<Weapon>();
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
    }

    public void AimWeapon(Vector2 mousePos)
    {
        if (_weapon == null) return;
        Vector3 aimDirection = (Vector3)mousePos - transform.position;

        AdjustWeaponRenderer();

        _disireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(_disireAngle, Vector3.forward);
    }

    private void AdjustWeaponRenderer()
    {
        if (_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_disireAngle > 90 || _disireAngle < -90);
        }
    }
}
