using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Axe : SubWeapon
{
    private float _throwForce;
    private float _spreadRange;
    private float _dropForce;
    private float _rotateSpeed;
    [SerializeField] private AnimationCurve _ease;

    private Vector3 _originPos;


    public override void StartAttack()
    {
        _originPos = transform.position;
        base.StartAttack();
        float x = Random.Range(-_spreadRange, _spreadRange);
        SetOrderInLayer(false);
        transform.DOJump(transform.position + new Vector3(x, -_dropForce), _throwForce, 1, _lifeTime).SetEase(Ease.InOutCubic);
        StartCoroutine(DelayOrderLayer());
    }

    public void FixedUpdate()
    {
        if (!_attackStart) return;

        float angle = transform.eulerAngles.z + Time.fixedDeltaTime * _rotateSpeed;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    private IEnumerator DelayOrderLayer()
    {
        yield return new WaitForSeconds(Mathf.Lerp(0f, _lifeTime, 0.4f));
        SetOrderInLayer(true);
    }

    public void InitAxe(float throwForce, float spreadRange, float dropForce, float rotateSpeed)
    {
        _throwForce = throwForce;
        _spreadRange = spreadRange;
        _dropForce = dropForce;
        _rotateSpeed = rotateSpeed;

        _attackStart = false;
    }
}
