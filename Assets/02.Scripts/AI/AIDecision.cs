using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{

    protected AIMoveData _moveData;
    protected EnemyAIBrain _enemyAIBrain;
    protected AIActionData _actionData;
    protected virtual void Awake()
    {
        _moveData = transform.GetComponentInParent<AIMoveData>();
        _enemyAIBrain = transform.GetComponentInParent<EnemyAIBrain>();
        _actionData = transform.GetComponentInParent<AIActionData>();

    }
    public abstract bool MakeDecision();
}
