using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubWeaponController : MonoBehaviour
{
    [SerializeField] protected ESubWeaponType _subWeaponType;
    [SerializeField] protected SubWeaponDataSO _weaponData;
    [SerializeField] protected LayerMask _enemyMask;

    public ESubWeaponType Type { get => _subWeaponType; }
    public bool IsActive { get => _isActive; set => _isActive = value; }

    private bool _isActive = false;

    private float _extraDelay = 0f;

    public void Init()
    {
        if (_weaponData.isSpawn)
        {
            PoolManager.Inst.CreatePool(_weaponData.prefab, 20);
        }

        ChlidInit();
    }

    protected virtual void ChlidInit()
    {

    }

    [ContextMenu("11")]
    public void ActiveWeapon()
    {
        if (_isActive) return;
        Init();
        _isActive = true;
        gameObject.SetActive(true);

        if (GameManager.Inst.OnUI)
        {
            GameManager.Inst.UI.OnUI.AddListener(WaitAttack);
        }
        else
        {
            StartCoroutine(LoopCoroutine());
        }
    }

    private void WaitAttack(bool onUI)
    {
        if (!onUI)
        {
            GameManager.Inst.UI.OnUI.RemoveListener(WaitAttack);
            StartCoroutine(LoopCoroutine());
        }
    }

    private IEnumerator LoopCoroutine()
    {
        while (_isActive && !GameManager.Inst.GameEnd)
        {
            EnterAction();

            if (_weaponData.needLifeTime && _weaponData.isInfinite == false)
            {
                
                yield return LifeTimeCoroutine();
            }

            else
            {
                TakeAction();
            }

            EndAction();

            yield return new WaitForSeconds(_weaponData.delayTime + _extraDelay);
        }
    }

    protected abstract void TakeAction();

    protected virtual void EnterAction() { }
    protected virtual void EndAction() { }

    protected IEnumerator DelayFunc(float delay,Action action)
    {
        _extraDelay = delay;
        yield return new WaitForSeconds(delay);

        action?.Invoke();
    }

    protected IEnumerator LifeTimeCoroutine()
    {
        float lifeTime = _weaponData.lifeTime;

        while(lifeTime >= 0f)
        {
            TakeAction();
            lifeTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

    }

    protected void TriggerEnter(Collider2D col, GameObject gameObject = null)
    {
        IHittable hit = col.transform.GetComponent<IHittable>();

        if (hit == null) return;

        if (_weaponData.isAttack)
        {
            hit.GetHit(_weaponData.damageAmount, gameObject == null ? this.gameObject : gameObject);
        }

        if (_weaponData.isCrowdCtrl)
        {
            hit.GetCrowdCtrl(_weaponData.crowdCtrlTypes, _weaponData.crowdCtrlAmount);
        }


    }
}
