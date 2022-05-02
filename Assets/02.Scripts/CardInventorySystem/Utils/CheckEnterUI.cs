using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class CheckEnterUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IUIEvent
{
    private Image _currentImage;

    public Action OnPointerUIEnter { get; set; }
    public Action OnPointerUIExit { get; set; }


    private void Start()
    {
        _currentImage = GetComponent<Image>();
        EventManager.StartListening(Constant.POINTDOWN_CARD, () => _currentImage.raycastTarget = true);
        EventManager.StartListening(Constant.POINTUP_CARD, () => _currentImage.raycastTarget = false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerUIEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerUIExit?.Invoke();
    }

}
