using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;

public class AgentInput : MonoBehaviour
{
    private AgentStateCheck _agentStateCheck = null;

    public UnityEvent<Vector2> OnPlayerMoveEvent;
    public UnityEvent<Vector2> OnPlayerMousePointEvent;
    public UnityEvent<Vector2> OnPlayerDashButtonPressEvent;
    public UnityEvent OnPlayerMousePressEvent;
    public UnityEvent OnESkillButtonPressEvent;
    public UnityEvent OnChangeCharacterEvent;

    private void Awake()
    {
        _agentStateCheck = GetComponent<AgentStateCheck>();
    }

    private void Update()
    {
        if (GameManager.Inst.OnUI || GameManager.Inst.GameEnd) return;

        GetMoveInput();
        GetMousePointInput();
        GetDashButtonInput();
        GetMouseButtonInput();
        GetESkillButtonInput();
        GetChangeCharacterInput();
    }

    private void GetMoveInput()
    {
        OnPlayerMoveEvent?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void GetMousePointInput()
    {
        OnPlayerMousePointEvent?.Invoke(MousePos);
    }

    private void GetDashButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnPlayerDashButtonPressEvent?.Invoke(MousePos);
    }

    private void GetMouseButtonInput()
    {
        if (Input.GetMouseButtonDown(0))
            OnPlayerMousePressEvent?.Invoke();
    }

    private void GetESkillButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnESkillButtonPressEvent?.Invoke();
            SkillCoolDown.StartTimer();
        }
    }

    private void GetChangeCharacterInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnChangeCharacterEvent?.Invoke();
            _agentStateCheck.StateReset();
            GameManager.Inst.ChangeCharacter();
        }
    }
}
