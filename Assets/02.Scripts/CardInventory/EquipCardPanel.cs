using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCardPanel : CardPanel
{
    private CombinePanel _parentCombinePanel;
    protected override void ChildInit()
    {
        _panelType = ECardPanelType.Equip;

        _parentCombinePanel = GetComponentInParent<CombinePanel>();
    }

    public override void ChangeCard(CardData cardData, bool isEffect = true)
    {
        base.ChangeCard(cardData, isEffect);

        if (_currentIdx < 2)
        {
            GameManager.Inst.SetPlayerableCardInfo(_currentCardData.ID);
        }
    }
}
