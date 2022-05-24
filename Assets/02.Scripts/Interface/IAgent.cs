using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgent
{
    public float Health { get; set; }
    public UnityEvent OnDie { get; set; }
    public UnityEvent OnGetHit { get; set; }
}
