using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooSpear : PoolableMono
{
    private void Update()
    {
        transform.position += transform.up * 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float damage = PlayerStatusManager.Inst.DynamicPlayerStatus.attackDamage;
            float criticalPercentage = PlayerStatusManager.Inst.DynamicPlayerStatus.criticalPercent;
            damage = damage / 100 * criticalPercentage;
            IHittable hittable = collision.GetComponent<IHittable>();
            hittable.GetHit(damage, gameObject);
        }
    }

    public override void Reset()
    {

    }
}
