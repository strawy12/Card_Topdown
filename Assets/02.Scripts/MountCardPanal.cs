using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MountCardPanal : CardPanal
{
    protected override void ChildInit()
    {
        _isHoldPanal = false;
    }

    protected override void ChildStart()
    {
    }
}
