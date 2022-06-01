using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardBarUI : BarUI
{
    protected override void SetGuageUI(float value)
    {
        if (value > 1f) 
        {
            _fillBar.transform.DOScaleX(1f, 0.3f).OnComplete(() =>
            {
                _fillBar.transform.localScale = new Vector3(0f, 1f, 1f);
                _fillBar.transform.DOScaleX(value - 1f, 0.3f).OnComplete(AutoFadeOut).SetDelay(0.1f);
            });
        }

        else
        {
            base.SetGuageUI(value);
        }
    }
}
