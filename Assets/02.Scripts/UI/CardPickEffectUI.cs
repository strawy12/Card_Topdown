using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardPickEffectUI : MonoBehaviour
{
    private RectTransform _rectTransform;
    private CardOutLineEffect _outlineEffect;
    private bool _startEffect;
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _outlineEffect = GetComponent<CardOutLineEffect>();
        EventManager.StartListening(Constant.TRIGGER_PICK_CARD, StartEffect);
        EventManager.StartListening(Constant.OPEN_INVENTORY, ResetUI);
        ResetUI();
    }

    private void StartEffect()
    {
        if (_startEffect) return;
        _outlineEffect.EffectStart();
        _startEffect = false;
        gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();

        seq.Append(_rectTransform.DOAnchorPosX(0f, 0.5f));
        seq.Append(_rectTransform.DOAnchorPosX(_rectTransform.rect.width, 0.4f).SetDelay(0.8f));
        seq.AppendCallback(ResetUI);
    }

    private void ResetUI()
    {
        if (gameObject.activeSelf == false) return;

        _rectTransform.anchoredPosition = new Vector2(_rectTransform.rect.width, _rectTransform.anchoredPosition.y);
        gameObject.SetActive(false);
        _outlineEffect.EffectStop();
        _startEffect = false;
    }
}
