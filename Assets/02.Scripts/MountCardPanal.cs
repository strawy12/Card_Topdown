using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MountCardPanal : CardPanal, IPointerClickHandler
{
    private static MountingUI _moutingUI;
    protected override void ChildInit()
    {
        if(_moutingUI == null)
        {
            _moutingUI = transform.GetComponentInParent<MountingUI>();
        }
        _isDeferPanal = false;
    }

    protected override void ChildStart()
    {
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isEmpty) return;


    }


}
