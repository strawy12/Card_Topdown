using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineTrap : PoolableMono
{
    [SerializeField] private float damage = 50f;

    private float time = 0f;
    [SerializeField] private float destroyTime = 5f;

    private void Update()
    {
        if (time >= destroyTime)
            PoolManager.Inst.Push(this);
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
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
