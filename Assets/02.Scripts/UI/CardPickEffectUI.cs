using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardPickEffectUI : MonoBehaviour
{
    private RectTransform _rectTransform;

    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        EventManager.StartListening(Constant.TRIGGER_PICK_CARD, StartEffect);
        EventManager.StartListening(Constant.OPEN_INVENTORY, ResetUI);
        ResetUI();
    }

    private void InitSequence()
    {


    }

    private void StartEffect()
    {
        gameObject.SetActive(true);
        Sequence seq = DOTween.Sequence();

        seq.Append(_rectTransform.DOAnchorPosX(0f, 0.5f));
        seq.Append(_rectTransform.DOAnchorPosX(_rectTransform.rect.width, 0.4f).SetDelay(0.8f));
        seq.AppendCallback(ResetUI);

        seq.Play();
    }

    private void ResetUI()
    {
        if (gameObject.activeSelf == false) return;

        _rectTransform.anchoredPosition = new Vector2(_rectTransform.rect.width, _rectTransform.anchoredPosition.y);
        gameObject.SetActive(false);
    }
}
