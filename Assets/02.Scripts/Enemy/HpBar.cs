using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class HpBar : MonoBehaviour
{
    [SerializeField]    
    private Transform _hpBar;

    [SerializeField]
    private SpriteGroupFade _spriteGroup;

    [SerializeField]
    private bool _autoFadeOut;

    private Coroutine _fadeOutCoroutine;

    [ContextMenu("Test")]
    public void Test()
    {
        HpBarGaugeSetting(0f);
    }

    public void HpBarGaugeSetting(float value)
    {
        if(_spriteGroup == null)
        {
            _spriteGroup = GetComponent<SpriteGroupFade>();
        }

        _spriteGroup.OIFFade(1f, 0.5f, () => SetGuageUI(value));
    }

    private void SetGuageUI(float value)
    {
        if (value < 0)
        {
            value = 0;
        }
        _hpBar.transform.DOScaleX(value, 0.3f).OnComplete(AutoFadeOut);
    }

    private void AutoFadeOut()
    {
        if(_fadeOutCoroutine != null)
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
