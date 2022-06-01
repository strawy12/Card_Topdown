using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class WaveController : MonoBehaviour
{
    [Header("Wave�� ���� ���� ���� ��� �־��ָ� ��")]
    public List<WaveDataSO> waves;

    public UnityEvent<int, int> OnWaveCountUpdated;
    public  UnityEvent<int, int> OnRemainEnemyUpdated;
    public UnityEvent OnClearAllWaves;
    public UnityEvent OnWaveEnded;
    #region wave�� ������� ������ �ϱ� ���Ѱ�
    //�ش� �κ��� ���̺갡 �������� �ٲ�ų�, ������ ������� ������ �� ����.
    private WaveDataSO[] wavesArr;
    private int waveIndex = 0;
    private int waveMaxEnemy = 0, remainEnemy = 0;

    public  int RemainEnemy
    {
        get => remainEnemy;
        set
        {
            remainEnemy = value;
            if(remainEnemy <= 0)
            {
                remainEnemy = 0;
                isWaveProcessing = false;
                OnWaveEnded?.Invoke();
            }
            OnRemainEnemyUpdated?.Invoke(remainEnemy, waveMaxEnemy);
        }
    }
    private bool isWaveProcessing = false;
    #endregion

    private void Start()
    {
        wavesArr = waves.ToArray();
    }
    private void SetMaxEnemy()
    {
        foreach(WavePattern pattern in wavesArr[waveIndex -1].patterns)
        {
            waveMaxEnemy += pattern.count;
        }
        remainEnemy = waveMaxEnemy;
        OnRemainEnemyUpdated?.Invoke(remainEnemy, waveMaxEnemy);
    }
    public void StartWave()
    {
        if (isWaveProcessing) return;
        waveMaxEnemy = 0;
        waveIndex++;
        if (waveIndex > waves.Count)
        {
            OnClearAllWaves?.Invoke();
            return;
        }
        SetMaxEnemy();
        StartCoroutine(StartWavePattern());
        OnWaveCountUpdated?.Invoke(waveIndex, waves.Count);

    }
    public IEnumerator StartWavePattern()
    {
        isWaveProcessing = true;
        foreach (WavePattern pattern in wavesArr[waveIndex -1].patterns)
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
            posX = posX == 1 ? posX = 0.99f : posX;
            posY = Random.Range(0f, 0.99f);
        }
        else
        {
            posX = Random.Range(0f, 0.99f);
            posY = Random.Range(0, 2);
            posY = posY == 1 ? posY = 0.99f : posY;
        }
        Vector2 pos = Camera.main.ViewportToWorldPoint(new Vector2(posX, posY));
        return pos;
    }

    public void SpawnEnemy(string monsterName,Vector2 pos)
    {
        Enemy monster = PoolManager.Inst.Pop(monsterName) as Enemy;
        monster.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
}
