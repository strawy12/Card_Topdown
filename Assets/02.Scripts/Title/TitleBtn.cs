using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class TitleBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill(true);
        transform.DOScale(Vector3.one * 1.3f, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill(true);
        transform.DOScale(Vector3.one, 0.5f);
    }
}
