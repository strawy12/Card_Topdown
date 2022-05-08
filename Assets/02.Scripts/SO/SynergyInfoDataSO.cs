using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GenealogyDefine;

[System.Serializable]
public class SynergyInfo
{
    public ESynergy type;
    public List<List<int>> infoList;
}


[CreateAssetMenu(fileName = "SynergyInfoDataSO", menuName = "SO/SynergyInfoDataSO")]
public class SynergyInfoDataSO : ScriptableObject
{
    [SerializeField] private SynergyInfo[] _syneargyInfoSO;
}
