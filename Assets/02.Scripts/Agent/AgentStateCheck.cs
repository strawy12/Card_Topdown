using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AgentStateCheck
{
    public bool IsDead { get; set; }

    public bool IsDashing { get; set; }

    public bool IsStop { get; set; }
}
