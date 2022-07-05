using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducedShield : Shield 
{
    protected override void GetHit(float damage, GameObject damageDealer)
    {
        var changeStat = _weaponData.changeStatList.Find(stat => stat.statType == EStatType.DamageReduction);
        damage *= changeStat.statAmount;

        OnHitShelid?.Invoke();
        if (_isDelay == false)
        {
            _isDelay = true;
            StartCoroutine(DelayFunc(_waitTime, () => ReleaseShield(isHit: true)));
        }

        _player.GetHitDamage(damage);
    }
}
