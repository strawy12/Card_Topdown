using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public string enemyName;
    public GameObject prefab;

    public float maxHealth = 10;
    public float damage = 1;
    public float attackDelay = 1f;
    public int defence = 0;
    public float cardGague = 1f;
    public float knockbackRegist = 0f;
}
