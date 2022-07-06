using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;
public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = PlayerRef.transform.position - transform.position; 
        _moveData.direction = dir.normalized;
        _moveData.pointOfInterest = PlayerRef.transform.position;

        _enemyAIBrain.Move(_moveData.direction, _moveData.pointOfInterest);
    }

}
