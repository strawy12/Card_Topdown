using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanaceDecision : AIDecision
{
    public float distance = 5f;

    public override bool MakeDecision()
    {
        float calc = Vector2.Distance(_enemyAIBrain.target.position, transform.position);
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
