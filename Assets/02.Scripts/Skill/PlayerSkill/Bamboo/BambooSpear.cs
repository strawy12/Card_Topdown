using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooSpear : PoolableMono
{
    private float time = 0f;
    private float destroyTime = 3f;

    private void Update()
    {
        if (time >= destroyTime)
            PoolManager.Inst.Push(this);
        time += Time.deltaTime;
        transform.position += transform.up * 0.5f;
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
            PoolManager.Inst.Push(this);
        }
    }

    public override void Reset()
    {
        time = 0;
    }
}
