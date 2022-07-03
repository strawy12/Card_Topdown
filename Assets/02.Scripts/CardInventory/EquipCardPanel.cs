using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCardPanel : CardPanel
{
    private CombinePanel _parentCombinePanel;
    protected override void ChildInit()
    {
        _panelType = ECardPanelType.Equip;
        _currentIdx = transform.parent.parent.GetSiblingIndex() - 1 + transform.GetSiblingIndex();

        _parentCombinePanel = transform.parent.parent.GetComponent<CombinePanel>();
    }

    public override void ChangeCard(CardData cardData, bool isEffect = true)
    {
        base.ChangeCard(cardData, isEffect);

        if (_currentIdx < 2)
        {
            GameManager.Inst.SetPlayerableCardInfo(_currentCardData.ID);
        }

        if(!_isEmpty)
        {
            _parentCombinePanel.ChangePanal();
        }
    }
}
