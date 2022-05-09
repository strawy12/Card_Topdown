using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInput : MonoBehaviour
{
    public UnityEvent OnInventoryKeyInput = new UnityEvent();
    void Update()
    {
        OnKeyInput();
    }

    private void OnKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryKeyInput?.Invoke();
        }
    }
}
