using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    private Vector2 _playerFaceDir;
    private float _currentAngle;

    [SerializeField] private float _speed;
    [SerializeField] private float _delay;
    [SerializeField] private float _daggerDelay;
    [SerializeField] private int _throwCnt;

    [SerializeField] private GameObject _daggerPrefab;

    private List<GameObject> _daggerObjectList = new List<GameObject>();

    private void Start()
    {

    }

    public void SetPlayerFaceDir(Vector2 dir)
    {
        _playerFaceDir = dir.normalized;
    }

    private void StartAttack()
    {
        _currentAngle = Mathf.Atan2(_playerFaceDir.y, _playerFaceDir.x) * Mathf.Rad2Deg - 90f;
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < _throwCnt; i++)
            {
                _daggerObjectList[i].SetActive(true);
            }
        }
    }
}
