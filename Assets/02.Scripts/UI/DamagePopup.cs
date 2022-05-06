using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(float damage,Vector3 pos,bool isCritical)
    {
        transform.position = pos;
        _textMesh.SetText(damage.ToString());
        if(isCritical)
        {
            _textMesh.color = Color.red;
            _textMesh.fontSize = 15f;
        }
        Sequence seq = DOTween.Sequence();
        seq.Join(_textMesh.DOFade(0, 1f));
       
    }
}
