using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckEnterUI : MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler
{
    private UnityEngine.UI.Image _currentImage;
    private void Start()
    {
        _currentImage = GetComponent<UnityEngine.UI.Image>();
        EventManager.StartListening(Constant.POINTDOWN_CARD, () => _currentImage.raycastTarget = true);
        EventManager.StartListening(Constant.POINTUP_CARD, () => _currentImage.raycastTarget = false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        EventManager.TriggerEvent(Constant.ENTER_UI); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        EventManager.TriggerEvent(Constant.EXIT_UI);
    }

}
