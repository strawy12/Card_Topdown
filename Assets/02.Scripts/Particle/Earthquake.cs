using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : PoolableMono
{
    private Animator _animator;
    public Vector2 hitCapsuleSize;
    private Enemy _enemy;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _audioClip;
    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
    }
    public void SapwnEffect(Enemy enemy, Vector2 attackpos)   
    {
        _enemy = enemy;
        transform.position = new Vector2(attackpos.x, attackpos.y);
        _animator.Play("Earthquake");
        _audioSource.Play();
    }
    public override void Reset()
    {
        //_animator.Play("Earthquake");
    }
    public void OnHittable()
    {
        Collider2D[] colliders = Physics2D.OverlapCapsuleAll(transform.position, hitCapsuleSize, CapsuleDirection2D.Vertical, 0);
        foreach(Collider2D collider in colliders)
        {
            if(collider.CompareTag("Player"))
            {
                IHittable hittable = collider.GetComponent<IHittable>();
                hittable?.GetHit(damage: _enemy.EnemyData.damage, damageDealer: _enemy.gameObject);
            }
        }
    }

    public void EndAnim()
    {
        PoolManager.Inst.Push(this);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, hitCapsuleSize);
    }
}
