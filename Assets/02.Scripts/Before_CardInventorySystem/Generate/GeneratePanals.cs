using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePanals : MonoBehaviour
{
    [SerializeField] private GameObject _panalTemp;
    [SerializeField] private int _generateCnt;

    private void Start()
    {
        Generate();

    }

    public void Generate()
    {
        GameObject panal;
        for (int i = 0; i < _generateCnt; i++)
        {
            panal = Instantiate(_panalTemp, _panalTemp.transform.parent);
            panal.gameObject.SetActive(true);
            ChildSettingPanal(panal);
        }
    }

    protected virtual void ChildSettingPanal(GameObject panal) { }

}
