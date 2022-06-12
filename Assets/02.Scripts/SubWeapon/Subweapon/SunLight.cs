using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SunLight : SubWeaponController
{
    // 추후 라인렌더러, 파티클 시스템 등으로 구현할 예정
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _radius;
    [SerializeField] private Vector2 _offset;

    [Space()]
    public bool DrawGizmo = false;
    protected override void ChlidInit()
    {
        if(_spriteRenderer == null)
        {
            _spriteRenderer = transform.Find("Effect").GetComponent<SpriteRenderer>();
        }

        _spriteRenderer.transform.localScale = Vector3.one * _radius;
        _spriteRenderer.transform.position = transform.position + (Vector3)_offset;

        if (_spriteRenderer.color.a != 0f)
        {
            _spriteRenderer.DOFade(0f, 0f);
        }

    }

    protected override void TakeAction()
    {
        if (_spriteRenderer.color.a == 0f)
        {
            _spriteRenderer.DOFade(1f, 0.5f);
        }

        Collider2D[] hitCols = Physics2D.OverlapCircleAll(transform.position + (Vector3)_offset, _radius, _enemyMask);

        foreach(var col in hitCols)
        {
            TriggerEnter(col, gameObject);
        }
    }

    protected override void EndAction()
    {
        _spriteRenderer.DOFade(0f, 0.5f);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(DrawGizmo)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position + (Vector3)_offset, _radius);

            Gizmos.color = Color.white;
        }
    }

#endif
}
