using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickTypeSelectUI : MonoCardUI
{
    public enum EPickType { Want, Random, Draw }

    [SerializeField] private Button[] _pickTypeButtons = new Button[3];

    private void Awake()
    {
        for(int i = 0; i < _pickTypeButtons.Length; i++)
        {
            EPickType type = (EPickType)i;
            _pickTypeButtons[i].onClick.AddListener(() => OnClickSelectBtn(type));
        }
    }

    public void ActiveUI()
    {
        CheckActiveButton();
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.6f).SetUpdate(true).SetEase(Ease.InOutElastic);
        GameManager.Inst.UI.PushPanal(gameObject);
    }

    private void CheckActiveButton()
    {
        _pickTypeButtons[(int)EPickType.Want].interactable = !InventoryManager.IsFull;
        _pickTypeButtons[(int)EPickType.Random].interactable = InventoryManager.EmptyPanalCount >= 2;
        _pickTypeButtons[(int)EPickType.Draw].interactable = InventoryManager.IsMounting;
    }

    public void UnActiveUI(System.Action action = null)
    {
        GameManager.Inst.UI.ClosePanal(gameObject, action);
    }


    public void OnClickSelectBtn(EPickType type)
    {
        string eventName = "";

        switch (type)
        {
            case EPickType.Want:
                eventName = Constant.ACTIVE_WANTPICK_UI;
                break;

            case EPickType.Random:
                eventName = Constant.TRIGGER_RANDOM_PICK;
                break;

            case EPickType.Draw:
                eventName = Constant.ACTIVE_DRAWPICK_UI;
                break;
        }

        UnActiveUI();
        EventManager.TriggerEvent(eventName);
    }


}