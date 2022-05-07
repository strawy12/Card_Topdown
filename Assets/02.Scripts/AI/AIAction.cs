using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData _actionData;
    protected EnemyAIBrain _enemyAIBrain;
    protected AIMoveData _moveData;
    private void Awake()
    {
        _enemyAIBrain = transform.GetComponentInParent<EnemyAIBrain>();
        _actionData = transform.GetComponentInParent<AIActionData>();
        _moveData = transform.GetComponentInParent<AIMoveData>();
        ChildAwake();
    }
    protected virtual void ChildAwake()
    {

    }
    public abstract void TakeAction();
}
