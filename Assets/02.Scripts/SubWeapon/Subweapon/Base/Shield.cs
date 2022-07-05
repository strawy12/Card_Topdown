using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Shield : SubWeaponController
{
    [SerializeField] protected float _shieldSize;
    [SerializeField] protected float _generateTime;
    [SerializeField] protected float _waitTime;
    [SerializeField] protected Transform _effect;
    [SerializeField] private bool _defendingCC;
    protected Player _player;
    protected bool _isDelay;

    protected UnityEvent OnHitShelid;

    protected override void ChlidInit()
    {
        _player = UtilDefine.PlayerRef;
    }

    protected override void TakeAction()
    {
        GenerateShield();
        StopAllCoroutines();
    }

    protected void GenerateShield()
    {
        if(_defendingCC)
        {
            _player.PushCCAtkShieldStack(GetCCAtk);
        }
        _player.PushHitShieldStack(GetHit);

        _effect.gameObject.SetActive(true);

        _effect.DOScale(Vector3.one * _shieldSize, _generateTime);
    }

    protected void ReleaseShield(bool isHit)
    {
        _effect.transform.localScale = Vector3.zero;
        _effect.gameObject.SetActive(false);
        _isDelay = false;

        if(isHit)
        {
            _player.PopHitShieldStack();
        }

        else
        {
            _player.PopCCAtkShieldStack();
        }


        StartCoroutine(DelayFunc(_weaponData.delayTime, GenerateShield));
    }

    protected virtual void GetHit(float damage, GameObject damageDealer)
    {
       
    }

    protected virtual void GetCCAtk(int types, float amount)
    {

    }

}
