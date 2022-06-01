using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    [SerializeField]
    private PoolableMono[] prefabs;

    [SerializeField] private float delay = 5f;

    void Start()
    {
        StartCoroutine(GenerateMonster());
    }

    private IEnumerator GenerateMonster()
    {
        while(true)
        {
            PoolableMono monster = PoolManager.inst.Pop(prefabs[Random.Range(0, prefabs.Length)].name);
            monster.gameObject.SetActive(true);

            yield return new WaitForSeconds(delay);
        }
    }
}
