using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickTypeSelectUI : PanalUI
{
    public enum PickType { Want, Random, Draw }

    public void OnClickSelectBtn(int type)
    {
        string eventName = "";

        switch ((PickType)type)
        {
            case PickType.Want:
                eventName = Constant.ACTIVE_WANTPICK_UI;
                break;

            case PickType.Random:
                eventName = Constant.TRIGGER_RANDOM_PICK;
                break;

            case PickType.Draw:
                eventName = Constant.ACTIVE_DRAWPICK_UI;
                break;
        }

        UnActiveUI();
        EventManager.TriggerEvent(eventName);
    }

   
}