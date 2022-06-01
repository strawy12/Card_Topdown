using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class WavePattern
{
    public EnemyDataSO monster;
    public float spawnDelay = 3f;
    public int count = 1;
    public float nextPatternDelay = 5f;
    public bool isRandomPos = true; // ����� �ʼ������� ���� ��ġ�� �������� ��ȯ�ϱ⿡ ���� �����������.
}

[CreateAssetMenu(menuName = "SO/Wave")]
public class WaveDataSO : ScriptableObject
{
    public List<WavePattern> patterns;
}
