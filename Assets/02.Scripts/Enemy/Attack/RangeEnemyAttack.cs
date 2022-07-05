using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RangeEnemyAttack : EnemyAttack
{
    [SerializeField]private projectileSO _projectileData;
    public Transform firePos;

    private AgentStateCheck _agentStateCheck = null;

    protected override void Awake()
    {
        base.Awake();
        _agentStateCheck = GetComponent<AgentStateCheck>();
    }

    public Coroutine attackCoroutine = null;
    public override void Attack(float damage)
    {
       if(!_waitBeforeNextAttack && !_enemy.IsStiff && !_agentStateCheck.IsStop)
        {
            _isAttacking = true;
            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

    public void StartSpawningProjectile()
    {
        SpawnProjectile(firePos.position, RotateToTarget());
    }
    public Quaternion RotateToTarget()
    {
        Vector3 dir = GetTarget().position - firePos.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);
        return rot;
    }

    public void SpawnProjectile(Vector3 pos, Quaternion rot)
    {
        EnemyProjectile projectile = PoolManager.Inst.Pop(_projectileData.prefab.name) as EnemyProjectile;
        projectile.SetPositionAndRotation(pos, rot);  
        projectile.ProjectileData = _projectileData;
        projectile._isChaging = true;
    }
}
