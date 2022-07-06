using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        _moveData.direction = Vector2.zero;
        _moveData.pointOfInterest = PlayerRef.transform.position;
        _enemyAIBrain.Move(_moveData.direction, _moveData.pointOfInterest);
        
        _enemyAIBrain.Attack();
    }
}
