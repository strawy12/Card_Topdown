using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UtilDefine;

public class FollowCam : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCam = null;

    private void Awake()
    {
        _virtualCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_virtualCam.Follow == null)
        {
            _virtualCam.Follow = PlayerRef.transform;
        }
    }
}
