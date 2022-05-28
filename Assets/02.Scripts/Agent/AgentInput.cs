using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UtilDefine;

public class AgentInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnPlayerMoveEvent;
    public UnityEvent<Vector2> OnPlayerMousePointEvent;
    public UnityEvent<Vector2> OnPlayerDashButtonPressEvent;
    public UnityEvent OnPlayerMousePressEvent;
    public UnityEvent OnESkillButtonPressEvent;


    private void Update()
    {
        if (GameManager.Inst.OnUI) return;

        GetMoveInput();
        GetMousePointInput();
        GetDashButtonInput();
        GetMouseButtonInput();
        GetESkillButtonInput();
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
            OnESkillButtonPressEvent?.Invoke();
    }
}
