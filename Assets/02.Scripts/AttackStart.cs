using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackStart : MonoBehaviour
{
    private void OnEnable()
    {
        transform.Rotate(Vector3.forward, 45, Space.Self);
        Turn();
    }

    private void Turn()
    {
        transform.DORotate(new Vector3(0, 0, -135), 0.25f, RotateMode.WorldAxisAdd).SetEase(Ease.OutQuad);
    }

}
