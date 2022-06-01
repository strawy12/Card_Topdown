using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ShakeCameraFeedback : Feedback
{
    [SerializeField] private CinemachineVirtualCamera _cmVCam;
    
    [SerializeField]
    [Range(0, 5f)] // 흔들리는 강도?(범위) // 흔들리는 속도?
    private float _ampulitude = 1, _intencity = 1;

    [SerializeField] // 지속시간
    private float _duration = 0.5f;

    // ampulitude와 intencity가있는 클래스
    private CinemachineBasicMultiChannelPerlin _noise;

    private void OnEnable()
    {
        if (_cmVCam == null)
            _cmVCam = UtilDefine.VCam;

        _noise = _cmVCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public override void CompleteFeedback()
    {
        StopAllCoroutines();
        _noise.m_AmplitudeGain = 0;
        // 어차피 범위가 0이면 속도가 빨라도 문제없어서 그런가 안썼네
        _noise.m_FrequencyGain = 0;
    }

    public override void CreateFeedback()
    {
        _noise.m_AmplitudeGain = _ampulitude;
        _noise.m_FrequencyGain = _intencity;
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float time = _duration;
        while (time > 0)
        {   // 범위를 시간에 따라 점점줄여서 흔들림이 멈추는 것을 표현함
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _ampulitude, time / _duration);
            yield return null;
            time -= Time.deltaTime;
        }
        // 흔들기가 끝났는데 amplitude가 0이 아니니까 0으로 바꿈
        _noise.m_AmplitudeGain = 0;
    }
}
