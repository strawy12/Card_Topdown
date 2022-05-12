using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected static BoxCollider2D boxCol2D;

    private void Awake()
    {
        boxCol2D = GetComponent<BoxCollider2D>();
        boxCol2D.enabled = false;
    }

    public void AttackAction()
    {
        boxCol2D.enabled = true;
    }

    public static void AttackStop()
    {
        boxCol2D.isTrigger = true;
        boxCol2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IHittable hittable = collision.GetComponent<IHittable>();
            hittable.GetHit(damage: 10, damageDealer: gameObject);
            boxCol2D.isTrigger = false;
            // 대충 맞는 유니티 이벤트
        }
    }
}
