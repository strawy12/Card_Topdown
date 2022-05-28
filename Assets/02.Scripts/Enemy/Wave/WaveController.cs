using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave의 종류 레벨 순서 대로 넣어주면 됨")]
    public List<WaveDataSO> waves;

    [Header("몬스터 나오는 자표의 최대/최소 값")]
    public Vector2 MaxPos;
    public Vector2 MinPos;

    #region wave를 순서대로 나오게 하기 위한값
    //해당 부분은 웨이브가 랜덤으로 바뀌거나, 순서가 사라지면 수정될 수 있음.
    private WaveDataSO[] wavesArr;
    private int waveIndex = 0;
    private bool isWaveProcessing = false;
    #endregion

    private void Start()
    {
        wavesArr = waves.ToArray();
        StartWave();
    }
    private void StartWave()
    {
        if (isWaveProcessing) return;
        StartCoroutine(StartWavePattern());
        waveIndex++;
    }
    public IEnumerator StartWavePattern()
    {
        foreach (WavePattern pattern in wavesArr[waveIndex].patterns)
        {
            for (int i = 0; i < pattern.count; i++)
            {
                float posX = Random.Range(MinPos.x, MaxPos.x);
                float posY = Random.Range(MinPos.y, MaxPos.y);
                Vector2 pos = new Vector2(posX, posY);
                Enemy monster = PoolManager.inst.Pop(pattern.monster.prefab.name) as Enemy;
                monster.transform.SetPositionAndRotation(pos, Quaternion.identity);

                yield return new WaitForSeconds(pattern.spawnDelay);
            }
            yield return new WaitForSeconds(pattern.nextPatternDelay);
        }
    }
}
