using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenealogyData 
{
    public GenealogyDefine.EGenealogy genealogyType;
    public int genealogyNum;

    public GenealogyData(GenealogyDefine.EGenealogy type, int num)
    {
        genealogyType = type;
        genealogyNum = num;
    }
}
