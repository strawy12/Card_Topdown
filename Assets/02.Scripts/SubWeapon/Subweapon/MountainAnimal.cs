using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainAnimal : SubWeaponController
{
    [SerializeField] private float _rotateFactor;
    [SerializeField] private float _detactRange;

    protected override void EnterAction()
    {
        SpawnAnimalObj();
    }


    protected override void TakeAction()
    {
    }

    private void SpawnAnimalObj()
    {
        MountainAnimalObject obj = PoolManager.Inst.Pop(_weaponData.prefab.name) as MountainAnimalObject;
        obj.transform.localScale = Vector3.zero;
        obj.transform.position = transform.position;

        obj.gameObject.SetActive(true);
        obj.Init(_weaponData.movementSpeed, _rotateFactor, _detactRange, _weaponData.lifeTime);
    }


}
