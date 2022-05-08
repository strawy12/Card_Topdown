using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountingUI : MonoBehaviour
{
    [SerializeField] private GenealogyCardPanel _genealogyCardPanalTemp;
    private bool _isEnterUI;


    void Start()
    {
        _isEnterUI = false;
        PEventManager.StartListening(Constant.POINTUP_CARD, ActiveMountingCard);
        SubscribeEvent();
        GenerateGenealogyPanals();
    }

    private void SubscribeEvent()
    {
        IUIEvent uiEvent = transform.Find("CheckEnterUI").GetComponent<IUIEvent>();
        uiEvent.OnPointerUIEnter += () => _isEnterUI = true;
        uiEvent.OnPointerUIExit += () => _isEnterUI = false;
    }

    private void GenerateGenealogyPanals()
    {
        GenealogyCardPanel panal;
        for (int i = 0; i < 5; i++)
        {
            panal = Instantiate(_genealogyCardPanalTemp, _genealogyCardPanalTemp.transform.parent);
            panal.transform.SetSiblingIndex(0);
            panal.Init();
            panal.gameObject.SetActive(true);
        }
    }

    private void ActiveMountingCard(Param param)
    {
        if (_isEnterUI)
        {
            PEventManager.TriggerEvent(Constant.ENTER_MOUNTING_UI, param);
        }

        else
        {
            EventManager.TriggerEvent(Constant.NOT_ENTER_MOUNTING_UI);
        }

    }
}
