using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ShakeCameraFeedback : Feedback
{
    [SerializeField] private CinemachineVirtualCamera _cmVCam;
    
    [SerializeField]
    [Range(0, 5f)] // ��鸮�� ����?(����) // ��鸮�� �ӵ�?
    private float _ampulitude = 1, _intencity = 1;

    [SerializeField] // ���ӽð�
    private float _duration = 0.5f;

    // ampulitude�� intencity���ִ� Ŭ����
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
        // ������ ������ 0�̸� �ӵ��� ���� ������� �׷��� �Ƚ��
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
        {   // ������ �ð��� ���� �����ٿ��� ��鸲�� ���ߴ� ���� ǥ����
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _ampulitude, time / _duration);
            yield return null;
            time -= Time.deltaTime;
        }
        // ���Ⱑ �����µ� amplitude�� 0�� �ƴϴϱ� 0���� �ٲ�
        _noise.m_AmplitudeGain = 0;
    }
}
