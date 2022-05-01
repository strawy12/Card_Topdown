using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/MoveData")]
public class MoveDataSO : ScriptableObject
{
    [Range(1, 10f)]
    public float maxSpeed = 5f;

    [Range(0.1f, 100f)]
    public float acceleration = 50f, deceleration = 50f;
}
