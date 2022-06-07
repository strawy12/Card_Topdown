using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WantPickPanal : Button
{
    private static WantPickCardUI _parentUI;
    private int _currentIdx;

    public void InitPanal()
    {
        if(_parentUI == null)
        {
            _parentUI = GetComponentInParent<WantPickCardUI>();
        }

        _currentIdx = transform.GetSiblingIndex();
        onClick.AddListener(ClickPanal);
    }

    public void ActivePanal()
    {
        //this.interactable = GameManager.Inst.ExistCard(_currentIdx + 1);
    }

    private void ClickPanal()
    {
        _parentUI.SelectMonthCard(_currentIdx + 1);
    }
}
