using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePanals : MonoBehaviour
{
    [SerializeField] private CardPanal _panalTemp;
    [SerializeField] private int _generateCnt;
    private CardPanal[] cardPanals;


    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        cardPanals = new CardPanal[_generateCnt];

        for (int i = 0; i < _generateCnt; i++)
        {
            cardPanals[i] = Instantiate(_panalTemp, _panalTemp.transform.parent);
            cardPanals[i].gameObject.SetActive(true);
            cardPanals[i].Init();
        }
    }
}
