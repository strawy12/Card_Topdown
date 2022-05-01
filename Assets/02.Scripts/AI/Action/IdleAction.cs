using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void TakeAction()
    {
        _moveData.direction = Vector2.zero;
        _moveData.pointOfInterest = transform.position;
        _enemyAIBrain.Move(_moveData.direction, _moveData.pointOfInterest);
    }
}
