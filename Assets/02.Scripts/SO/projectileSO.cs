using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "SO/projectileData")]
public class projectileSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public int damage = 5;
    public float speed = 1;
    public Material material;
    public float lifeTime = 2f;
    public GameObject particlePrefab;
}
