using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePanals : MonoBehaviour
{
    [SerializeField] private CardPanal _panalTemp;
    [SerializeField] private int _generateCnt;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        CardPanal panal;
        for (int i = 0; i < _generateCnt; i++)
        {
            panal = Instantiate(_panalTemp, _panalTemp.transform.parent);
            ChildSettingPanal(panal);
            panal.gameObject.SetActive(true);
            panal.Init();
        }
    }

    protected virtual void ChildSettingPanal(CardPanal panal) { }

}
