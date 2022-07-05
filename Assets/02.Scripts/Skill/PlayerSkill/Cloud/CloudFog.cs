using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFog : PoolableMono
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private float increaseDamage = 1f;

    [SerializeField] private float destroyTime = 5f;
    private float time = 0f;

    [SerializeField] private float _hitInterval = 1f;

    List<Enemy> enemyList = new List<Enemy>();

    private void Update()
    {
        if (time >= destroyTime)
            PoolManager.Inst.Push(this);
        time += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy.IsHitCloud == true) return;
            StartCoroutine(HitCoroutine(collision));
        }
    }



    private IEnumerator HitCoroutine(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        enemy.IsHitCloud = true;
        _damage = Mathf.Round(time) * increaseDamage;
        IHittable hittable = collision.GetComponent<IHittable>();
        hittable.GetHit(_damage, gameObject);
        enemyList.Add(enemy);
        yield return new WaitForSeconds(_hitInterval);
        enemy.IsHitCloud = false;
    }

    public override void Reset()
    {
        foreach(Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.IsHitCloud = false;
            }
        }
        time = 0;
    }
}
