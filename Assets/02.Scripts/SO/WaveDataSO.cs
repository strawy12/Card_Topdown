using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class WavePattern
{
    public EnemyDataSO monster;
    public float spawnDelayTime = 0.2f;
    public int count = 1; 
    public bool isRandomPos = true; // ����� �ʼ������� ���� ��ġ�� �������� ��ȯ�ϱ⿡ ���� �����������.
}

[CreateAssetMenu(menuName = "SO/Wave")]
public class WaveDataSO : ScriptableObject
{
    public List<WavePattern> patterns;
}
