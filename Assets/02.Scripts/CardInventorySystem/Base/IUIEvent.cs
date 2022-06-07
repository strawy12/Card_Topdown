using UnityEngine;
using System;

public interface IUIEvent
{
    public Action<Param> OnPointerUpUIEnter { get; set; }
    public Action<Param> OnPointerUpUINotEnter { get; set; }
}
