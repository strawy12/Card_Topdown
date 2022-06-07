using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAttack : MonoBehaviour
{
    [SerializeField] 
    protected float _delay = 0.8f;

    public UnityEvent OnTriggerAttack;
    protected AgentStateCheck _agentStateCheck = null;

    private void Awake()
    {
        _agentStateCheck = GetComponentInParent<AgentStateCheck>();
        ChildAwake();
    }

    protected virtual void ChildAwake()
    {

    }

    public void StartAttack()
    {
        if (_agentStateCheck.IsAttack == true) return;
        _agentStateCheck.IsAttack = true;
        SpawnAttackEffect();
        StartCoroutine(Delay());
    }

    protected virtual void SpawnAttackEffect()
    {
        OnTriggerAttack?.Invoke();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _agentStateCheck.IsAttack = false;
    }

}