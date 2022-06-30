using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRaser : PoolableMono
{
    [SerializeField] private LayerMask layerMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.position += transform.up * 3f;
        }
    }

    public override void Reset()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
    }
}
