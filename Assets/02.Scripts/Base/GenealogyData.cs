using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GenealogyData 
{
    public int genealogyNum;

    public GenealogyData( int num)
    {
        genealogyNum = num;
    }

    public GenealogyData()
    {
        genealogyNum = 0;
    }
}
