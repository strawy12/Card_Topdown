using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunExplosion : SubWeaponController
{
    [SerializeField] private float _explosionRange = 0.7f;
    [SerializeField] private float _spawnRange = 3f;

    private List<SunExplosionObject> _sunExplosionObjectList = new List<SunExplosionObject>();

    public bool DrawGizmo;

    protected override void EnterAction()
    {
        SpawnExplosionObj();
    }

    protected override void TakeAction()
    {
        foreach(var obj in _sunExplosionObjectList)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(obj.transform.position, obj.transform.localScale.x, _enemyMask);

            foreach (var hit in hits)
            {
                TriggerEnter(hit, gameObject);
            }
        }
    }

    protected override void EndAction()
    {
        foreach (var obj in _sunExplosionObjectList)
        {
            obj.Reset();
        }

        _sunExplosionObjectList.Clear();
    }

    private Vector3 RandomCircleRangePos()
    {
        float angle = Random.Range(0f, 180f) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        float randRange = Random.Range(1f, _spawnRange);

        Vector3 pos = new Vector3(x, y, 0f).normalized * randRange;


        return pos;
    }

    private void SpawnExplosionObj()
    {
        SunExplosionObject obj = PoolManager.Inst.Pop(_weaponData.prefab.name) as SunExplosionObject;
        _sunExplosionObjectList.Add(obj);
        obj.transform.localScale = Vector3.zero;
        obj.transform.position = transform.position + RandomCircleRangePos();

        obj.gameObject.SetActive(true);
        obj.StartEffect(_explosionRange, _weaponData.lifeTime);
    }

    private void ReleaseObj(SunExplosionObject obj)
    {
        _sunExplosionObjectList.Remove(obj);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.red;

            foreach (var obj in _sunExplosionObjectList)
            {
                Gizmos.DrawWireSphere(obj.transform.position, obj.transform.localScale.x);
            }

            Gizmos.color = Color.white;
        }
    }

#endif
}
