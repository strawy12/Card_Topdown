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
        _rigid.gravityScale = 0f;
    }

    [ContextMenu("Ω√¿€")]
    public void StartAttack()
    {

        float randRot = Random.Range(-0.5f, 0.5f);
        Vector2 dir = new Vector2(randRot, 1f);
        _rigid.AddForce(dir.normalized * _throwForce);
        _rigid.gravityScale = 1f;







        _rigid.AddTorque(randRot * _throwForce);
    }
}
