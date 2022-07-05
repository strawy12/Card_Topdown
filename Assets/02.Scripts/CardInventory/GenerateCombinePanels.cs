using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCombinePanels : MonoBehaviour
{
    [SerializeField] private CombinePanel _cardPanelTemp;
    [SerializeField] private int _generateCount;

    private void Start()
    {
        GeneratePanel();
    }

    private void GeneratePanel()
    {
        CombinePanel    panel = null;

        for (int i = 0; i < _generateCount; i++)
        {
            panel = Instantiate(_cardPanelTemp, _cardPanelTemp.transform.parent);

            panel.Init();
            panel.transform.SetSiblingIndex(0);
            panel.gameObject.SetActive(true);
        }

    }
}
