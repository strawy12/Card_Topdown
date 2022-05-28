using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Wave�� ���� ���� ���� ��� �־��ָ� ��")]
    public List<WaveDataSO> waves;

    [Header("���� ������ ��ǥ�� �ִ�/�ּ� ��")]
    public Vector2 MaxPos;
    public Vector2 MinPos;

    #region wave�� ������� ������ �ϱ� ���Ѱ�
    //�ش� �κ��� ���̺갡 �������� �ٲ�ų�, ������ ������� ������ �� ����.
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
