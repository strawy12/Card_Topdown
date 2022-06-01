using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteGroupFade : MonoBehaviour
{

    private SpriteRenderer[] _spriteRenderers;
    private Coroutine _fadeCoroutine;

    private float _duration;
    private float _endValue;

    public float alpha;
    private float _startValue;
    private bool _fadeOn;

    private Action _callBackAction;

    public void OIFFade(float value, float duration, Action callbackAction = null)
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        _endValue = value;
        _duration = duration;
        _callBackAction = callbackAction;
        _fadeOn = _endValue > alpha;
        _startValue = alpha;

        _fadeCoroutine = StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        if (_spriteRenderers == null || _spriteRenderers.Length <= 0)
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        float time;

        if (_fadeOn)
        {
            time = 0f;
        }
        else
        {
            time = _duration;
        }

        while ((_fadeOn && time <= _duration) || (!_fadeOn && time >= 0f))
        {
            if (_fadeOn)
            {
                alpha = Mathf.Lerp(_startValue, _endValue, time / _duration);
            }

            else
            {
                alpha = Mathf.Lerp(_endValue, _startValue, time / _duration);
            }

            foreach (var renderer in _spriteRenderers)
            {
                Color color = renderer.color;
                color.a = alpha;

                renderer.color = color;
            }

            yield return null;

            if(_fadeOn)
            {
                time += Time.deltaTime;
            }

            else
            {
                time -= Time.deltaTime;
            }
        }

        alpha = _endValue;
        _duration = 0f;
        _fadeCoroutine = null;
        _callBackAction?.Invoke();
    }

    public void OIFKill()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
    }

    public void OIFComplete()
    {
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        foreach (var renderer in _spriteRenderers)
        {
            Color color = renderer.color;
            color.a = _endValue;
            renderer.color = color;
        }

        alpha = _endValue;
        _callBackAction?.Invoke();
    }

    public void Reset()
    {
        if (_spriteRenderers == null || _spriteRenderers.Length <= 0)
        {
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        alpha = Mathf.Clamp(alpha, 0f, 1f);

        foreach (var renderer in _spriteRenderers) 
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}
