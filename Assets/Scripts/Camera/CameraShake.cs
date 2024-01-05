using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float ShakeIntensity = 1f;
    private float ShakeTime = 0.2f;
    private float timer;
    private CinemachineBasicMultiChannelPerlin _cbmcp;

    private void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        StopShake();
    }

    public void ShakeCamera()
    {
        // Use the instance of the cinemachineVirtualCamera to get the CinemachineBasicMultiChannelPerlin component.
        _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = ShakeIntensity;
        timer = ShakeTime;
    }

    void StopShake()
    {
        // Use the instance of the cinemachineVirtualCamera to get the CinemachineBasicMultiChannelPerlin component.
        _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
        timer = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera();
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                StopShake();
            }
        }
    }
}
