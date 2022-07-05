using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CardInventoryManager.Inst.SetCanEquipCard(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardInventoryManager.Inst.SetCanEquipCard(false);
    }
}