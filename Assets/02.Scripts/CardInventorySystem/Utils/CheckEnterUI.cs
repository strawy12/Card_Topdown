using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class CheckEnterUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IUIEvent
{
    private Image _currentImage;

    public Action<Param> OnPointerUpUIEnter { get; set; }
    public Action<Param> OnPointerUpUINotEnter { get; set; }

    private bool _isEnter = false;


    private void Start()
    {
        _currentImage = GetComponent<Image>();

        if(_currentImage.color.a != 0f)
        {
            Color color = _currentImage.color;
            color.a = 0f;
            _currentImage.color = color;
        }

        EventManager.StartListening(Constant.POINTDOWN_CARD, () => _currentImage.raycastTarget = true);
        EventManager.StartListening(Constant.POINTUP_CARD, () => _currentImage.raycastTarget = false);
        PEventManager.StartListening(Constant.POINTUP_CARD, EnterEvent);
    }

    private void EnterEvent(Param param)
    {
        if(_isEnter)
        {
            OnPointerUpUIEnter?.Invoke(param);
        }

        else
        {
            OnPointerUpUINotEnter?.Invoke(param);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _isEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isEnter = false;
    }

}
