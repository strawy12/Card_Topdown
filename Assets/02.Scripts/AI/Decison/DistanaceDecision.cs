using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;
public class DistanaceDecision : AIDecision
{
    public float distance = 5f;

    public override bool MakeDecision()
    {
        float calc = Vector2.Distance(PlayerRef.transform.position, transform.position);
        if(calc <= distance)
        {
            if(_actionData.targetSpotted == false)
            {
                _actionData.targetSpotted = true;
            }
        }
        else
        {
            _actionData.targetSpotted = false;
        }
        return _actionData.targetSpotted;
    }
}
