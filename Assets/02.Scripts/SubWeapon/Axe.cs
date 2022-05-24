using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private float _throwForce;
    private Rigidbody2D _rigid;
    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void StartAttack()
    {
        float randRot = Random.Range(-1f, 1f);
        Vector2 dir = new Vector2(randRot, 1f);
        _rigid.AddForce(dir.normalized * _throwForce);
        _rigid.AddTorque(randRot * _throwForce);
    }
}
