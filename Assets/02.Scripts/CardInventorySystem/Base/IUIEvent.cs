using UnityEngine;
using System;

public interface IUIEvent
{
    public Action OnPointerUIEnter { get; set; }
    public Action OnPointerUIExit { get; set; } 
}
