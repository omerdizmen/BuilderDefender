using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineBasicMultiChannelPerlin _cameraPerlin;
    private float _timer;
    private float _timerMax;
    private float _startingIntensity;

    private void Awake()
    {
        Instance = this; 
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if(_timer < _timerMax)
        {
            _timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(_startingIntensity, 0f, _timer / _timerMax);
            _cameraPerlin.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intensity, float timer)
    {
        _timerMax = timer;
        _timer = 0;
        _startingIntensity = intensity;
        _cameraPerlin.m_AmplitudeGain = intensity;

    }
}
