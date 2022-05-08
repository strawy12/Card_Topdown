using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    protected SpriteRenderer _weaponRenderer;

    private void Awake()
    {
        _weaponRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite(bool value)
    {
        Vector3 localScale = new Vector3(1, value ? -1 : 1, 1);

        transform.localScale = localScale;
    }
}
