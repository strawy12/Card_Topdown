using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = _enemyAIBrain.target.position - transform.position; 
        _moveData.direction = dir.normalized;
        _moveData.pointOfInterest = _enemyAIBrain.target.position;

        _enemyAIBrain.Move(_moveData.direction, _moveData.pointOfInterest);
    }

}
