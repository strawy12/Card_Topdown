using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Transform _playerTrm;

    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            return _playerTrm;
        }
    }
}