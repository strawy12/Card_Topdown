using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class WantPickCardUI : PanalUI
{
    private List<WantPickPanal> _panalList;


    private void Awake()
    {
        EventManager.StartListening(Constant.ACTIVE_WANTPICK_UI, ActiveUI);
        // TODO : UI Panal Stack 구조 구현

        InitPanals();

        gameObject.SetActive(false);
    }

    private void InitPanals()
    {
        _panalList = GetComponentsInChildren<WantPickPanal>().ToList();

        _panalList.ForEach(panal => panal.InitPanal());
    }

    protected override void ChildActiveUI()
    {
        ActivePanals();
    }


    private void ActivePanals()
    {
        _panalList.ForEach(panal => panal.ActivePanal());
    }

    private void SetPanalInteractableAll(bool interactable)
    {
        _panalList.ForEach(panal => panal.interactable = interactable);
    }
    public void SelectMonthCard(int monthNum)
    {
        SetPanalInteractableAll(false);
        Param param = new Param();
        param.iParam = monthNum;

        UnActiveUI(() => PEventManager.TriggerEvent(Constant.TRIGGER_WANT_PICK, param));
    }
}
