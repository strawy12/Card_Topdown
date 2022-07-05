using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
public class WaveController : MonoBehaviour
{
    [Header("Wave의 종류 레벨 순서 대로 넣어주면 됨")]
    public List<WaveDataSO> waves;

    public UnityEvent OnClearAllWaves;

    #region wave를 순서대로 나오게 하기 위한값
    //해당 부분은 웨이브가 랜덤으로 바뀌거나, 순서가 사라지면 수정될 수 있음.
    private WaveDataSO[] wavesArr;
    private int waveIndex = 0;
    private int remainEnemy = 0;
    private bool isClearAllwaved =false;
    public int RemainEnemy
    {
        get => remainEnemy;
        set
        {
            remainEnemy = value;
            if (remainEnemy <= 0 && isClearAllwaved)
            {
                OnClearAllWaves?.Invoke();
            }            
        }
    }
    #endregion

    private void Start()
    {
        wavesArr = waves.ToArray();
        StartWave();

        Invoke("StartWave", 2f);
    }
   
    public void StartWave()
    {
        waveIndex++;
        if (waveIndex > waves.Count)
        {
            isClearAllwaved = true;
            return;
        }
        StartCoroutine(StartWavePattern());
    }
    public IEnumerator StartWavePattern()
    {
        foreach (WavePattern pattern in wavesArr[waveIndex -1].patterns)
        {
            if (GameManager.Inst.GameEnd) yield break;

            for (int i = 0; i < pattern.count; i++)
            {
                Vector2 pos = RandomCameraSideVector();
                SpawnEnemy(pattern.monster.prefab.name, pos);
                yield return new WaitForSeconds(pattern.spawnDelay);
            }
            yield return new WaitForSeconds(pattern.nextPatternDelay);
        }
        yield return new WaitForSeconds(3f);
        StartWave();
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
        pos.x = Mathf.Clamp(pos.x,-180, 215);
        pos.y = Mathf.Clamp(pos.y, -125, 125);
        return pos;
    }

    public void SpawnEnemy(string monsterName,Vector2 pos)
    {
        Enemy monster = PoolManager.Inst.Pop(monsterName) as Enemy;
        remainEnemy++;
        monster.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }
}
