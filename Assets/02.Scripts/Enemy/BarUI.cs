using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class BarUI : MonoBehaviour
{
    [SerializeField]
    private Transform _fillBar;

    [SerializeField]
    private SpriteGroupFade _spriteGroup;

    [SerializeField]
    private bool _autoFadeOut;

    private Coroutine _fadeOutCoroutine;

    public float FillAmout
    {
        get => _fillBar.transform.localScale.x;
        set
        {
            _fillBar.transform.localScale = new Vector3(value, 1f, 1f);
        }
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

    private void SetGuageUI(float value)
    {
        if (value < 0)
        {
            value = 0;
        }

        if(value > 1f)
        {
            _fillBar.transform.DOScaleX(1f, 0.3f).OnComplete(() =>
            {
                _fillBar.transform.localScale = new Vector3(0f, 1f, 1f);
                _fillBar.transform.DOScaleX(value - 1f, 0.3f).OnComplete(AutoFadeOut).SetDelay(0.1f);
            });

            return;
        }

        _fillBar.transform.DOScaleX(value, 0.3f).OnComplete(AutoFadeOut);
    }

    private void AutoFadeOut()
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
