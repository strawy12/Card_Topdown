using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class WaveController : MonoBehaviour
{
    [Header("Wave�� ���� ���� ���� ��� �־��ָ� ��")]
    public List<WaveDataSO> waves;

    public UnityEvent<int, int> OnWaveEnded;
    public UnityEvent<int, int> OnEnemyDie;
    #region wave�� ������� ������ �ϱ� ���Ѱ�
    //�ش� �κ��� ���̺갡 �������� �ٲ�ų�, ������ ������� ������ �� ����.
    private WaveDataSO[] wavesArr;
    private int waveIndex = 0;
    private int waveMaxEnemy = 0, remainEnemy = 0;

    private bool isWaveProcessing = false;
    #endregion

    private void Start()
    {
        wavesArr = waves.ToArray();
        StartWave();

    }
    private void SetMaxEnemy()
    {
        foreach(WavePattern pattern in wavesArr[waveIndex].patterns)
        {
            waveMaxEnemy += pattern.count;
        }
        remainEnemy = waveMaxEnemy;
        OnEnemyDie?.Invoke(remainEnemy, waveMaxEnemy);
    }
    private void StartWave()
    {
        if (isWaveProcessing) return;
        waveMaxEnemy = 0;
        SetMaxEnemy();
        StartCoroutine(StartWavePattern());
        waveIndex++;
        OnWaveEnded?.Invoke(waveIndex, waves.Count);
    }
    public IEnumerator StartWavePattern()
    {
        foreach (WavePattern pattern in wavesArr[waveIndex].patterns)
        {
            for (int i = 0; i < pattern.count; i++)
            {
                Vector2 pos = RandomCameraSideVector();
                SpawnEnemy(pattern.monster.prefab.name, pos);
                yield return new WaitForSeconds(pattern.spawnDelay);
            }
            yield return new WaitForSeconds(pattern.nextPatternDelay);
        }
    }

    private static Vector2 RandomCameraSideVector()
    {
        float posX, posY;
        int randomSpawnCameraSideDir = Random.Range(0, 2);
        if (randomSpawnCameraSideDir == 1)
        {
            posX = Random.Range(0, 2);
            posY = Random.Range(0f, 1f);
        }
        else
        {
            posX = Random.Range(0f, 1f);
            posY = Random.Range(0, 2);
        }
        Vector2 pos = Camera.main.ViewportToWorldPoint(new Vector2(posX, posY));
        return pos;
    }

    public void SpawnEnemy(string monsterName,Vector2 pos)
    {
        Enemy monster = PoolManager.inst.Pop(monsterName) as Enemy;
        monster.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
    public void EnemyDie()
    {
        Debug.Log(remainEnemy);
        remainEnemy--;
        OnEnemyDie?.Invoke(remainEnemy, waveMaxEnemy);
    }
}
