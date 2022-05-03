using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnPlayerMoveEvent;
    public UnityEvent<Vector2> OnPlayerMousePointEvent;
    public UnityEvent<Vector2> OnPlayerDashButtonPressEvent;
    public UnityEvent OnPlayerMousePressEvent;

    private void Update()
    {
        GetMoveInput();
        GetMousePointInput();
        GetDashButtonInput();
        GetMouseButtonInput();
    }

    private void GetMoveInput()
    {
        OnPlayerMoveEvent?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void GetMousePointInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;

        Vector2 mouseWorldPosition = MainCam.ScreenToWorldPoint(mousePosition);
        OnPlayerMousePointEvent?.Invoke(mouseWorldPosition);
    }

    private void GetDashButtonInput()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;

        Vector2 mouseWorldPosition = MainCam.ScreenToWorldPoint(mousePosition);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            OnPlayerDashButtonPressEvent?.Invoke(mouseWorldPosition);
    }

    private void GetMouseButtonInput()
    {
        if (Input.GetMouseButtonDown(0))
            OnPlayerMousePressEvent?.Invoke();
    }
}
