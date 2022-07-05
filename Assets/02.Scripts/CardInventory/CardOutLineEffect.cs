using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardOutLineEffect : MonoBehaviour
{
    [SerializeField] private float _effectSpeed = 1.35f;   
    private Material _material;
    private bool _shouldEffect;

    private float _currentAngle = 0f;
    
    void Start()
    {
    }

    public void EffectStart()
    {
        if(_material == null)
        {
            _material = GetComponent<Image>().material;
        }

        _shouldEffect = true;
    }

    public void EffectStop()
    {
        _shouldEffect = false;
    }

    void Update()
    {
        if (!_shouldEffect) return;

        _currentAngle += _effectSpeed * Time.deltaTime;

        if (_currentAngle >= 360f)
        {
            _currentAngle = 0f;
        }

        _material.SetFloat("_Angle", _currentAngle);
    }
}
