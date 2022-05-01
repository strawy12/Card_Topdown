using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetWalkAnimation(bool value)
    {
        _animator.SetBool("Walk", value);
    }
    public void WalkAnimationStart(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }
    public void StartAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
    public void FaceDirection(Vector2 pointerInput)
    {
        Vector3 dir = (Vector3)pointerInput - transform.position;
        Vector3 result = Vector3.Cross(Vector2.up, dir);

        if(result.z < 0 )
        {
            _spriteRenderer.flipX = true;
        }
        else if(result.z > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }
}
