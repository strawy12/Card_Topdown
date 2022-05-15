using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField]    
    private Transform _hpBar;

    public void HpBarGaugeSetting(float value)
    {
        if(value < 0)
        {
            value = 0;
        }
        _hpBar.transform.DOScaleX(value, 0.3f);
    }
}
