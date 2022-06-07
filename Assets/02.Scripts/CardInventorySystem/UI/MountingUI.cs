using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MountingUI : MonoBehaviour
{
    [SerializeField] private GenealogyCardPanal _genealogyCardPanalTemp;


    void Start()
    {
        IUIEvent uIEvent = transform.Find("CheckEnterUI").GetComponent<IUIEvent>();
        SubscribeEvent(uIEvent);
        GenerateGenealogyPanals();
    }

    private void SubscribeEvent(IUIEvent uiEvent)
    {
        uiEvent.OnPointerUpUIEnter += ActiveMountingCard;
        uiEvent.OnPointerUpUINotEnter += UnActiveMountingCard;

    }

    private void GenerateGenealogyPanals()
    {
        GenealogyCardPanal panal;
        for (int i = 0; i < 5; i++)
        {
            panal = Instantiate(_genealogyCardPanalTemp, _genealogyCardPanalTemp.transform.parent);
            panal.transform.SetSiblingIndex(0);
            panal.Init();
            panal.gameObject.SetActive(true);
            panal.OnPointerUpUIEnter += ActiveMountingCard;
            panal.OnPointerUpUINotEnter += ActiveMountingCard;
        }
    }

    private void ActiveMountingCard(Param param)
    {
        PEventManager.TriggerEvent(Constant.ENTER_MOUNTING_UI, param);
    }

    private void UnActiveMountingCard(Param param)
    {
        PEventManager.TriggerEvent(Constant.NOT_ENTER_MOUNTING_UI, param);
    }
}
