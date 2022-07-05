using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCardPanels : MonoBehaviour
{
    [SerializeField] private CardPanel _cardPanelTemp;
    [SerializeField] private int _generateCount;

    private void Start()
    {
        GeneratePanel();
    }

    private void GeneratePanel()
    {
        CardPanel panel = null;

        for (int i = 0; i < _generateCount; i++)
        {
            panel = Instantiate(_cardPanelTemp, _cardPanelTemp.transform.parent);

            panel.Init();
            panel.gameObject.SetActive(true);
        }

    }
}
