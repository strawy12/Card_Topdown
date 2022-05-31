using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOutLineEffect : MonoBehaviour
{
    [SerializeField] private float _effectSpeed = 1.35f;   
    private Material material;
    private bool _shouldEffect;

    private float _currentAngle = 0f;
    
    void Start()
    {
        return;
        material = GetComponent<SpriteRenderer>().material;
    }

    public void EffectStart()
    {
        _shouldEffect = true;
        material.SetFloat("_Thickness", 10f);
    }

    public void EffectStop()
    {
        _shouldEffect = false;
        material.SetFloat("_Thickness", 0f);
    }

    //void Update()
    //{
    //    if (!_shouldEffect) return;

    //    _currentAngle += _effectSpeed * Time.deltaTime;

    //    if(_currentAngle >= 360f)
    //    {
    //        _currentAngle = 0f;
    //    }

    //    material.SetFloat("_Angle", _currentAngle);
    //}
}
