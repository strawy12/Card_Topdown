using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;
public class EnemyAIBrain : MonoBehaviour
{
    [field: SerializeField] public UnityEvent<Vector2> OnMovement { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }
    [field: SerializeField] public UnityEvent OnAttack { get; set; }
    [SerializeField] private AIState _currentState;

    public void Attack()
    {
        OnAttack?.Invoke(); 
    }
    public void Move(Vector2 moveDir, Vector2 targetPos)
    {
        OnMovement?.Invoke(moveDir);
        OnPointerPositionChanged?.Invoke(targetPos);
    }

    public void ChangeState(AIState state)
    {
        _currentState = state;
    }
    private void Update()
    {
        if (GameManager.Inst.GameEnd || GameManager.Inst.OnUI) return;

        if(PlayerRef == null)
        {
            OnMovement?.Invoke(Vector2.zero);
        }
        else
        {
            _currentState.UpdateState();
        }   
    }
}
