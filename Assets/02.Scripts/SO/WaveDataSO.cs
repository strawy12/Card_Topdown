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
    public bool isRandomPos = true; // 현재는 필수적으로 몬스터 위치를 랜더으로 소환하기에 따로 사용하지않음.
}

[CreateAssetMenu(menuName = "SO/Wave")]
public class WaveDataSO : ScriptableObject
{
    public List<WavePattern> patterns;
}
