using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleShield : Shield
{
    protected override void GetHit(float damage, GameObject damageDealer)
    {
        OnHitShelid?.Invoke();
        if (_isDelay == false)
        {
            _isDelay = true;
            StartCoroutine(DelayFunc(_waitTime, () => ReleaseShield(isHit: true)));
        }
    }
}
