using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class BarUI : MonoBehaviour
{
    [SerializeField]
    protected Transform _fillBar;

    [SerializeField]
    protected SpriteGroupFade _spriteGroup;

    [SerializeField]
    private bool _autoFadeOut;

    protected Coroutine _fadeOutCoroutine;

    public float FillAmout
    {
        get => _fillBar.transform.localScale.x;
    }

    public void GaugeBarGaugeSetting(float value)
    {
        if (_spriteGroup == null)
        {
            _spriteGroup = GetComponent<SpriteGroupFade>();
        }

        if (_spriteGroup.alpha != 1f)
        {
            _spriteGroup.OIFFade(1f, 0.5f, () => SetGuageUI(value));
        }

        else
        {
            SetGuageUI(value);
        }
    }

    protected virtual void SetGuageUI(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);

        _fillBar.transform.DOScaleX(value, 0.3f).OnComplete(AutoFadeOut);
    }

    protected void AutoFadeOut()
    {
        if (_autoFadeOut == false) return;
        if (gameObject.activeSelf == false) return;

        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
        }

        _fadeOutCoroutine = StartCoroutine(AutoFadeOutCoroutine());
    }

    private IEnumerator AutoFadeOutCoroutine()
    {
        yield return new WaitForSeconds(3f);

        _spriteGroup.OIFFade(0f, 0.5f);
    }
}
