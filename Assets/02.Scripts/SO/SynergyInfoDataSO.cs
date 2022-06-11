using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SynergyDataList
{
    public List<int> dataList;

    public int Count { get => dataList.Count; }

    public SynergyDataList()
    {
        dataList = new List<int>();
    }
}

[System.Serializable]
public class SynergyInfo
{
    public List<SynergyDataList> infoList;

    public List<int> this[int idx]
    {
        get => infoList[idx].dataList;

        set => infoList[idx].dataList = value;
    }

    public void Add(SynergyDataList elementList)
    {
        if (elementList == null||elementList.Count == 0) return;

        if (infoList == null)
        {
            infoList = new List<SynergyDataList>();
        }

        infoList.Add(elementList);
    }

}


[CreateAssetMenu(fileName = "SynergyInfoDataSO", menuName = "SO/SynergyInfoDataSO")]
public class SynergyInfoDataSO : ScriptableObject
{
    [SerializeField] private List<SynergyInfo> _syneargyInfoSO;

    public SynergyInfo this[int idx]
    {
        get
        {
            if(_syneargyInfoSO.Count <= idx)
            {
                _syneargyInfoSO.Add(new SynergyInfo());
            }

            return _syneargyInfoSO[idx];
        }

        set
        {
            if (_syneargyInfoSO.Count <= idx)
            {
                _syneargyInfoSO.Add(new SynergyInfo());
            }

            _syneargyInfoSO[idx] = value;
        }
    }
}
