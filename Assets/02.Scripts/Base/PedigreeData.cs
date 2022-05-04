using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PedigreeData 
{
    public Define.EPedigree pedigreeType;
    public int pedigreeNum;

    public PedigreeData(Define.EPedigree type, int num)
    {
        pedigreeType = type;
        pedigreeNum = num;
    }
}
