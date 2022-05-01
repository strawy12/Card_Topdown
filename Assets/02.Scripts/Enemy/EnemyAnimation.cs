using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour
{
    protected Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

}
